using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cargo.Application.Interfaces;
using Cargo.Application.DTOs.Vehicles;
using Cargo.Domain.Entities;
using AutoMapper;
using Cargo.Web.Areas.Admin.Models.VehicleViewModels;
using System.Linq;
using Cargo.Application.Mapping.Helpers;
using Cargo.Application.DTOs.Common;

namespace Cargo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(
            IVehicleService vehicleService,
            ICompanyService companyService,
            IMapper mapper,
            ILogger<VehicleController> logger)
        {
            _vehicleService = vehicleService;
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Admin/Vehicle
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string search = "")
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    vehicles = vehicles.Where(v =>
                        v.Make.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        v.Model.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        v.VIN.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        v.RegistrationNumber.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var totalCount = vehicles.Count();
                var pagedVehicles = vehicles
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var model = new VehicleListViewModel
                {
                    Vehicles = pagedVehicles,
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                    PageSize = pageSize,
                    SearchTerm = search,
                    TotalCount = totalCount
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading vehicles list");
                return View(new VehicleListViewModel { Vehicles = new List<VehicleDto>() });
            }
        }

        // GET: Admin/Vehicle/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading vehicle details for ID {VehicleId}", id);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Vehicle/Create
        public async Task<IActionResult> Create()
        {
           
            await PopulateCompanyDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel model)
        {
            // Ensure PlateNumber is initialized to prevent null reference issues
            if (model.PlateNumber == null)
            {
                model.PlateNumber = new PlateNumberViewModel();
            }

            // Explicit validation for all required fields
            if (string.IsNullOrWhiteSpace(model.Make))
                ModelState.AddModelError("Make", "Make is required");

            if (string.IsNullOrWhiteSpace(model.VehicleModel))
                ModelState.AddModelError("Model", "Model is required");

            if (model.Year < 1980 || model.Year > 2100)
                ModelState.AddModelError("Year", "Year must be between 1980 and 2100");

            if (string.IsNullOrWhiteSpace(model.VIN))
                ModelState.AddModelError("VIN", "VIN is required");
            else if (model.VIN.Length != 17)
                ModelState.AddModelError("VIN", "VIN must be exactly 17 characters");

            if (string.IsNullOrWhiteSpace(model.RegistrationNumber))
                ModelState.AddModelError("RegistrationNumber", "Registration number is required");

            if (string.IsNullOrWhiteSpace(model.FuelType))
                ModelState.AddModelError("FuelType", "Fuel type is required");

            if (model.Capacity <= 0)
                ModelState.AddModelError("Capacity", "Capacity must be greater than 0");

            if (model.OwnerCompanyId == Guid.Empty)
                ModelState.AddModelError("OwnerCompanyId", "Owner company is required");

            // PlateNumber validation
            if (string.IsNullOrWhiteSpace(model.PlateNumber.Value))
                ModelState.AddModelError("PlateNumber.Value", "Plate value is required");
            if (string.IsNullOrWhiteSpace(model.PlateNumber.PlateType))
                ModelState.AddModelError("PlateNumber.PlateType", "Plate type is required");
            if (string.IsNullOrWhiteSpace(model.PlateNumber.IssuingAuthority))
                ModelState.AddModelError("PlateNumber.IssuingAuthority", "Issuing authority is required");

            if (!ModelState.IsValid)
            {
                await PopulateCompanyDropdown();
                return View(model);
            }

            try
            {
                // Check for duplicate VIN
                var existingVehicles = await _vehicleService.GetAllVehiclesAsync();
                if (existingVehicles.Any(v => v.VIN.Equals(model.VIN, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("VIN", "A vehicle with this VIN already exists.");
                    await PopulateCompanyDropdown();
                    return View(model);
                }

                var vehicleDto = new VehicleCreateDto
                {
                    Make = model.Make,
                    Model = model.VehicleModel,
                    Year = model.Year,
                    Color = model.Color,
                    VIN = model.VIN,
                    RegistrationNumber = model.RegistrationNumber,
                    PlateNumber = new PlateNumberDto
                    {
                        Value = model.PlateNumber.Value,
                        IssuingAuthority = model.PlateNumber.IssuingAuthority,
                        PlateType = model.PlateNumber.PlateType
                    },
                    FuelType = model.FuelType,
                    Capacity = model.Capacity,
                    OwnerCompanyId = model.OwnerCompanyId
                };

                await _vehicleService.CreateVehicleAsync(vehicleDto);

                TempData["SuccessMessage"] = "Vehicle created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vehicle");
                ModelState.AddModelError("", "An error occurred while creating the vehicle. Please try again.");
                await PopulateCompanyDropdown();
                return View(model);
            }
        }

        // GET: Admin/Vehicle/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                var model = new VehicleEditViewModel
                {
                    Id = vehicle.Id,
                    Make = vehicle.Make,
                    VehicleModel = vehicle.Model,
                    Year = vehicle.Year,
                    Color = vehicle.Color,
                    VIN = vehicle.VIN,
                    RegistrationNumber = vehicle.RegistrationNumber,
                    PlateNumber = new  PlateNumberViewModel
                    {
                        Value = vehicle.PlateNumber.Value,
                        IssuingAuthority = vehicle.PlateNumber.IssuingAuthority,
                        PlateType = vehicle.PlateNumber.PlateType
                    },
                    FuelType = vehicle.FuelType,
                    Capacity = vehicle.Capacity,
                    OwnerCompanyId = vehicle.OwnerCompanyId
                };

                await PopulateCompanyDropdown();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading vehicle for edit: {VehicleId}", id);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateCompanyDropdown();
                return View(model);
            }

            try
            {
                var vehicleDto = new VehicleUpdateDto
                {
                    Make = model.Make,
                    Model = model.VehicleModel,
                    Year = model.Year,
                    Color = model.Color,
                    VIN = model.VIN,
                    RegistrationNumber = model.RegistrationNumber,
                    PlateNumber = new PlateNumberDto
                    {
                        Value = model.PlateNumber.Value,
                        IssuingAuthority = model.PlateNumber.IssuingAuthority,
                        PlateType = model.PlateNumber.PlateType
                    },
                    FuelType = model.FuelType,
                    Capacity = model.Capacity, 
                };

                await _vehicleService.UpdateVehicleAsync(id, vehicleDto);

                TempData["SuccessMessage"] = "Vehicle updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vehicle: {VehicleId}", id);
                ModelState.AddModelError("", "An error occurred while updating the vehicle. Please try again.");
                await PopulateCompanyDropdown();
                return View(model);
            }
        }

        // GET: Admin/Vehicle/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading vehicle for delete: {VehicleId}", id);
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(id);
                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Vehicle deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vehicle: {VehicleId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the vehicle. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Vehicle/QuickCreate
        public async Task<IActionResult> QuickCreate(Guid companyId, bool isModal = false)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var model = new VehicleCreateViewModel
            {
                OwnerCompanyId = companyId
            };

            if (isModal || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_QuickCreateModal", model);
            }

            ViewBag.CompanyName = company.Name;
            await PopulateCompanyDropdown();
            return View(model);
        }

        // POST: Admin/Vehicle/QuickCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuickCreate(VehicleCreateViewModel model)
        {
            bool isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                );

                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Validation failed",
                        errors = errors,
                        errorCount = ModelState.ErrorCount
                    });
                }

                await PopulateCompanyDropdown();
                return View(model);
            }

            // Check for duplicate VIN
            var existingVehicles = await _vehicleService.GetAllVehiclesAsync();
            if (existingVehicles.Any(v => v.VIN.Equals(model.VIN, StringComparison.OrdinalIgnoreCase)))
            {
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = "VIN already exists",
                        errors = new { VIN = "A vehicle with this VIN already exists." }
                    });
                }

                ModelState.AddModelError("VIN", "A vehicle with this VIN already exists.");
                await PopulateCompanyDropdown();
                return View(model);
            }

            // Check for duplicate registration number
            if (existingVehicles.Any(v => v.RegistrationNumber.Equals(model.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
            {
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Registration number already exists",
                        errors = new { RegistrationNumber = "A vehicle with this registration number already exists." }
                    });
                }

                ModelState.AddModelError("RegistrationNumber", "A vehicle with this registration number already exists.");
                await PopulateCompanyDropdown();
                return View(model);
            }

            try
            {
                var vehicleDto = new VehicleCreateDto
                {
                    Make = model.Make,
                    Model = model.VehicleModel,
                    Year = model.Year,
                    Color = model.Color,
                    VIN = model.VIN,
                    RegistrationNumber = model.RegistrationNumber,
                    PlateNumber = new PlateNumberDto
                    {
                        Value = model.PlateNumber.Value,
                        IssuingAuthority = model.PlateNumber.IssuingAuthority,
                        PlateType = model.PlateNumber.PlateType
                    },
                    FuelType = model.FuelType,
                    Capacity = model.Capacity,
                    OwnerCompanyId = model.OwnerCompanyId
                };

                var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicleDto);
                
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = true,
                        message = $"Vehicle {model.Make} {model.VehicleModel} ({model.Year}) added successfully!",
                        data = new
                        {
                            id = createdVehicle.Id,
                            make = model.Make,
                            model = model.VehicleModel,
                            year = model.Year,
                            vin = model.VIN,
                            registrationNumber = model.RegistrationNumber
                        },
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }

                TempData["SuccessMessage"] = $"Vehicle {model.Make} {model.VehicleModel} added successfully to company!";
                return RedirectToAction("Index", "Company");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vehicle");
                
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Server error: {ex.Message}",
                        error = ex.Message,
                        stackTrace = ex.StackTrace
                    });
                }

                ModelState.AddModelError(string.Empty, "An error occurred while creating the vehicle.");
                await PopulateCompanyDropdown();
                return View(model);
            }
        }

        private async Task PopulateCompanyDropdown()
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync();
                ViewBag.Companies = companies.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error populating company dropdown");
                ViewBag.Companies = new List<SelectListItem>();
            }
        }
    }
}
