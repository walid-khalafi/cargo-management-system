using Microsoft.AspNetCore.Mvc;
using Cargo.Application.Interfaces;
using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Application.DTOs.Company;
using Cargo.Web.Areas.Admin.Models.AssignmentViewModels;
using System;
using System.Linq;
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
    }

   
}
