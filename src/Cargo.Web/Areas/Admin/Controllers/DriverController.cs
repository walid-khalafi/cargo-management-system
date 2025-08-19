using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cargo.Application.Interfaces;
using Cargo.Application.DTOs.Driver;
using AutoMapper;
using Cargo.Web.Areas.Admin.Models.DriverViewModels;
using System.Threading.Tasks;
using Cargo.Domain.ValueObjects;
using Cargo.Application.DTOs.Common;

namespace Cargo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public DriverController(IDriverService driverService, ICompanyService companyService, IMapper mapper)
        {
            _driverService = driverService;
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET: Admin/Driver
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string search = "")
        {
            try
            {
                var drivers = await _driverService.GetAllDriversAsync();

                // Log for debugging
                Console.WriteLine($"Found {drivers.Count()} drivers");

                if (!string.IsNullOrWhiteSpace(search))
                {
                    drivers = drivers.Where(d =>
                        d.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        d.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        d.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        d.LicenseNumber.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var totalCount = drivers.Count();
                var pagedDrivers = drivers
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var model = new DriverListViewModel
                {
                    Drivers = pagedDrivers,
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
                // Log the actual exception
                Console.WriteLine($"Error in Driver Index: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                var model = new DriverListViewModel
                {
                    Drivers = new List<DriverDto>(),
                    CurrentPage = 1,
                    TotalPages = 1,
                    PageSize = pageSize,
                    SearchTerm = search,
                    TotalCount = 0
                };
                return View(model);
            }
        }

        // GET: Admin/Driver/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }


            var model = new DriverViewModel
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Email = driver.Email,
                PhoneNumber = driver.PhoneNumber,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseType = driver.LicenseType,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                DateOfBirth = driver.DateOfBirth,
                YearsOfExperience = driver.YearsOfExperience,
                Status = driver.Status,
                CompanyId = driver.CompanyId
            };

            return View(model);
        }

        // GET: Admin/Driver/Create
        public async Task<IActionResult> Create()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            ViewBag.Companies = companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }

        // GET: Admin/Driver/QuickCreate
        public async Task<IActionResult> QuickCreate(Guid companyId, bool isModal = false)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var model = new DriverCreateViewModel
            {
                CompanyId = companyId
            };

            if (isModal || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_QuickCreateModal", model);
            }

            ViewBag.CompanyName = company.Name;
            return View(model);
        }

        // POST: Admin/Driver/QuickCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuickCreate(DriverCreateViewModel model)
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

                var company = await _companyService.GetCompanyByIdAsync(model.CompanyId);
                ViewBag.CompanyName = company?.Name ?? "Unknown Company";
                return View(model);
            }

            // Check for duplicate email
            var existingDriver = await _driverService.IsEmailUniqueAsync(model.Email);
            if (!existingDriver)
            {
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Email already exists",
                        errors = new { Email = "A driver with this email address already exists." }
                    });
                }

                ModelState.AddModelError("Email", "A driver with this email address already exists.");
                var company = await _companyService.GetCompanyByIdAsync(model.CompanyId);
                ViewBag.CompanyName = company?.Name ?? "Unknown Company";
                return View(model);
            }

            try
            {
                var address = new Address(
                    model.Address.Country,
                    model.Address.State,
                    model.Address.City,
                    model.Address.Street,
                    model.Address.ZipCode
                );

                var driverDto = new DriverCreateDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = address.ToString(),
                    LicenseNumber = model.LicenseNumber,
                    LicenseType = model.LicenseType,
                    LicenseExpiryDate = model.LicenseExpiryDate,
                    DateOfBirth = model.DateOfBirth,
                    YearsOfExperience = model.YearsOfExperience,
                    CompanyId = model.CompanyId
                };

                var createdDriver = await _driverService.CreateDriverAsync(driverDto);
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = true,
                        message = $"Driver {model.FirstName} {model.LastName} added successfully!",
                        data = new
                        {
                            id = createdDriver.Id,
                            name = $"{model.FirstName} {model.LastName}",
                            email = model.Email,
                            phone = model.PhoneNumber,
                            licenseNumber = model.LicenseNumber
                        },
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }

                TempData["SuccessMessage"] = $"Driver {model.FirstName} {model.LastName} added successfully to company!";
                return RedirectToAction("Index", "Company");
            }
            catch (InvalidOperationException ex)
            {
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Business rule violation",
                        errors = new { Email = ex.Message }
                    });
                }

                ModelState.AddModelError("Email", ex.Message);
                var company = await _companyService.GetCompanyByIdAsync(model.CompanyId);
                ViewBag.CompanyName = company?.Name ?? "Unknown Company";
                return View(model);
            }
            catch (Exception ex)
            {
                if (isAjaxRequest)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Server error :{ex.Message}",
                        error = ex.Message,
                        stackTrace = ex.StackTrace
                    });
                }

                ModelState.AddModelError(string.Empty, "An error occurred while creating the driver.");
                var company = await _companyService.GetCompanyByIdAsync(model.CompanyId);
                ViewBag.CompanyName = company?.Name ?? "Unknown Company";
                return View(model);
            }
        }

        // POST: Admin/Driver/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var companies = await _companyService.GetAllCompaniesAsync();
                ViewBag.Companies = companies.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            // Check for duplicate email
            var existingDriver = await _driverService.IsEmailUniqueAsync(model.Email);
            if (!existingDriver)
            {
                ModelState.AddModelError("Email", "A driver with this email address already exists.");

                var companies = await _companyService.GetAllCompaniesAsync();
                ViewBag.Companies = companies.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            var address = new Address(model.Address.Country, model.Address.State, model.Address.City, model.Address.Street, model.Address.ZipCode);

            var driverDto = new DriverCreateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = address.ToString(),
                LicenseNumber = model.LicenseNumber,
                LicenseType = model.LicenseType,
                LicenseExpiryDate = model.LicenseExpiryDate,
                DateOfBirth = model.DateOfBirth,
                YearsOfExperience = model.YearsOfExperience,
                CompanyId = model.CompanyId
            };

            try
            {
                await _driverService.CreateDriverAsync(driverDto);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Email", ex.Message);

                var companies = await _companyService.GetAllCompaniesAsync();
                ViewBag.Companies = companies.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }
        }

        // GET: Admin/Driver/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            var companies = await _companyService.GetAllCompaniesAsync();
            ViewBag.Companies = companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();



            var addressDto = _mapper.Map<AddressDto>(Address.Parse(driver.Address));

            var model = new DriverUpdateViewModel
            {
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Email = driver.Email,
                PhoneNumber = driver.PhoneNumber,
                Address = addressDto,
                LicenseNumber = driver.LicenseNumber,
                LicenseType = driver.LicenseType,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                DateOfBirth = driver.DateOfBirth,
                YearsOfExperience = driver.YearsOfExperience,
                Status = driver.Status,
                CompanyId = driver.CompanyId
            };


            return View(model);
        }

        // POST: Admin/Driver/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DriverUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var companies = await _companyService.GetAllCompaniesAsync();
            ViewBag.Companies = companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }




            var address = new Address(model.Address.Country, model.Address.State, model.Address.City, model.Address.Street, model.Address.ZipCode);

            var driverDto = new DriverUpdateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = address.ToString(),
                LicenseNumber = model.LicenseNumber,
                LicenseType = model.LicenseType,
                LicenseExpiryDate = model.LicenseExpiryDate,
                DateOfBirth = model.DateOfBirth,
                YearsOfExperience = model.YearsOfExperience,
                CompanyId = model.CompanyId
            };

            await _driverService.UpdateDriverAsync(id, driverDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Driver/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }


            var model = new DriverViewModel
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Email = driver.Email,
                PhoneNumber = driver.PhoneNumber,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseType = driver.LicenseType,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                DateOfBirth = driver.DateOfBirth,
                YearsOfExperience = driver.YearsOfExperience,
                Status = driver.Status,
                CompanyId = driver.CompanyId
            };


            return View(model);
        }

        // POST: Admin/Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _driverService.DeleteDriverAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
