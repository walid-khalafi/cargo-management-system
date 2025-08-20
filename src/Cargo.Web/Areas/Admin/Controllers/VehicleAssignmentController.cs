using Microsoft.AspNetCore.Mvc;
using Cargo.Application.Interfaces;
using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Application.DTOs.Company;
using Cargo.Application.DTOs.Driver;
using Cargo.Web.Areas.Admin.Models.AssignmentViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Domain.Enums;

namespace Cargo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleAssignmentController : Controller
    {
        private readonly IDriverVehicleAssignmentService _assignmentService;
        private readonly ICompanyService _companyService;
        private readonly IVehicleService _vehicleService;
        private readonly IDriverService _driverService;

        public VehicleAssignmentController(
            IDriverVehicleAssignmentService assignmentService,
            ICompanyService companyService,
            IVehicleService vehicleService,
            IDriverService driverService)
        {
            _assignmentService = assignmentService;
            _companyService = companyService;
            _vehicleService = vehicleService;
            _driverService = driverService;
        }

        // Step 1: Company Selection
        public async Task<IActionResult> SelectCompany()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return View(companies);
        }

        // Step 2: Vehicle List with Assignment Status
        public async Task<IActionResult> VehicleList(Guid companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            var vehicles = await _vehicleService.GetVehiclesByCompanyAsync(companyId);
            var assignments = await _assignmentService.GetAllAssignmentsAsync();
            
            var viewModel = new VehicleAssignmentViewModel
            {
                CompanyId = companyId,
                CompanyName = company.Name,
                Vehicles = vehicles.ToList(),
                Assignments = assignments.ToList()
            };
            
            return View(viewModel);
        }

        // Step 3: Driver List with Assignment Status
        public async Task<IActionResult> DriverList(Guid companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            var drivers = await _driverService.GetDriversByCompanyAsync(companyId);
            var assignments = await _assignmentService.GetAllAssignmentsAsync();
            
            var viewModel = new DriverAssignmentViewModel
            {
                CompanyId = companyId,
                CompanyName = company.Name,
                Drivers = drivers.ToList(),
                Assignments = assignments.ToList()
            };
            
            return View(viewModel);
        }

        // Step 4: Create Assignment
        public async Task<IActionResult> Create(Guid companyId)
        {
            var viewModel = new CreateAssignmentViewModel
            {
                CompanyId = companyId,
                Vehicles = (await _vehicleService.GetVehiclesByCompanyAsync(companyId)).ToList(),
                Drivers = (await _driverService.GetDriversByCompanyAsync(companyId)).ToList()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDriverVehicleAssignmentDto assignmentDto, Guid companyId)
        {
            if (ModelState.IsValid)
            {
                await _assignmentService.CreateAssignmentAsync(assignmentDto);
                return RedirectToAction(nameof(VehicleList), new { companyId });
            }
            
            return View(assignmentDto);
        }

        // API Endpoints for Modal
        [HttpGet]
        public async Task<IActionResult> GetAvailableDrivers(Guid companyId)
        {
            try
            {
                var drivers = await _driverService.GetDriversByCompanyAsync(companyId);
                var assignments = await _assignmentService.GetAllAssignmentsAsync();
                
                // Get only drivers who don't have active assignments
                var availableDrivers = drivers.Where(d => 
                    !assignments.Any(a => a.DriverId == d.Id && a.Status == AssignmentStatus.Active))
                    .Select(d => new
                    {
                        id = d.Id,
                        name = $"{d.FirstName} {d.LastName}",
                        licenseNumber = d.LicenseNumber,
                        phoneNumber = d.PhoneNumber
                    });
                
                return Json(new { success = true, drivers = availableDrivers });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error loading drivers" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAssignmentAjax([FromBody] CreateDriverVehicleAssignmentDto assignmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { 
                        success = false, 
                        message = "Invalid data provided",
                        errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage))
                    });
                }

                // Check if vehicle already has an active assignment
                var existingAssignments = await _assignmentService.GetAllAssignmentsAsync();
                if (existingAssignments.Any(a => a.VehicleId == assignmentDto.VehicleId && a.Status == AssignmentStatus.Active))
                {
                    return Json(new { 
                        success = false, 
                        message = "This vehicle already has an active assignment" 
                    });
                }

                // Check if driver already has an active assignment
                if (existingAssignments.Any(a => a.DriverId == assignmentDto.DriverId && a.Status == AssignmentStatus.Active))
                {
                    return Json(new { 
                        success = false, 
                        message = "This driver already has an active assignment" 
                    });
                }

                await _assignmentService.CreateAssignmentAsync(assignmentDto);
                
                return Json(new { 
                    success = true, 
                    message = "Driver assigned successfully"
                });
            }
            catch (Exception ex)
            {
                return Json(new { 
                    success = false, 
                    message = "Error creating assignment: " + ex.Message 
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckVehicleAssignment(Guid vehicleId)
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignmentsAsync();
                var hasActiveAssignment = assignments.Any(a => a.VehicleId == vehicleId && a.Status == AssignmentStatus.Active);
                
                return Json(new { hasActiveAssignment });
            }
            catch (Exception ex)
            {
                return Json(new { hasActiveAssignment = false, message = "Error checking assignment" });
            }
        }
    }
}
