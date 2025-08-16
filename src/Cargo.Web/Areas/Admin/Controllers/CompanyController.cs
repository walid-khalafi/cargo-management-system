using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cargo.Application.Interfaces;
using Cargo.Application.DTOs.Company;
using Cargo.Domain.Entities;
using AutoMapper;
using Cargo.Web.Areas.Admin.Models.CompanyViewModels;
using System.Linq;
using Cargo.Application.Mapping.Helpers;
using Cargo.Domain.ValueObjects;

namespace Cargo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(
            ICompanyService companyService,
            IMapper mapper,
            ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Admin/Company
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string search = "")
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    companies = companies.Where(c =>
                        c.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase) ||
                        c.RegistrationNumber.Contains(search, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var totalCount = companies.Count();
                var pagedCompanies = companies
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var model = new CompanyListViewModel
                {
                    Companies = pagedCompanies.ToList(),
                    CurrentPage = page,
                    TotalPages = (int)System.Math.Ceiling(totalCount / (double)pageSize),
                    PageSize = pageSize,
                    SearchTerm = search,
                    TotalCount = totalCount
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading companies list");
                return View(new CompanyListViewModel { Companies = new List<CompanyDto>() });
            }
        }

        // GET: Admin/Company/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                return View(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading company details for ID {CompanyId}", id);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Company/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateViewModel model)
        {
            // Check model state before doing any processing
            if (!ModelState.IsValid)
            {
                // Return the same view with validation messages if the model is invalid
                return View(model);
            }

            try
            {
                // ✅ Construct the Address ValueObject from the AddressViewModel
                var address = new Address(model.Address.Country,
                    model.Address.State,
                    model.Address.City,
                    model.Address.Street,
                    model.Address.ZipCode);


                // ✅ Construct the TaxProfile ValueObject from the TaxProfileViewModel
                var taxProfile = new TaxProfile(
                    model.TaxProfile.GstRate,
                    model.TaxProfile.QstRate,
                    model.TaxProfile.PstRate,
                    model.TaxProfile.HstRate,
                    model.TaxProfile.CompoundQstOverGst
                );

                // ✅ Map the data into a DTO for the Application Layer
                // Using ToString() here because CompanyCreateDto stores these as string
                // In a more robust design, you'd have AddressDto and TaxProfileDto instead of strings
                var companyDto = new CompanyCreateDto
                {
                    Name = model.Name,
                    RegistrationNumber = model.RegistrationNumber,
                    Address = MappingHelper.FormatAddress(address),      // Formatted address string
                    TaxProfile = MappingHelper.FormatTaxProfile(taxProfile)    // Formatted tax profile string
                };

                // Call the application service to create the company
                await _companyService.CreateCompanyAsync(companyDto);

                // Store a success message in TempData for display after redirect
                TempData["SuccessMessage"] = "Company created successfully!";

                // Redirect to Index to prevent form re-submission (PRG Pattern)
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                _logger.LogError(ex, "Error creating company");

                // Add a generic error message for the user
                ModelState.AddModelError("", "An error occurred while creating the company. Please try again.");

                // Return the same view so the user can correct and resubmit
                return View(model);
            }
        }

        // GET: Admin/Company/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {

            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                ViewData["Id"] = id;

                var address = MappingHelper.ParseAddress(company.Address);
                var taxProfile = MappingHelper.ParseTaxProfile(company.TaxProfile);


                var model = new CompanyEditViewModel
                {
                    Name = company.Name,
                    RegistrationNumber = company.RegistrationNumber,
                    Address = new Models.CommonViewModels.AddressViewModel()
                    {
                        City = address.City,
                        Country = address.Country,
                        State = address.State,
                        ZipCode = address.ZipCode,
                        Street = address.Street,
                    },
                    TaxProfile = new TaxProfileViewModel()
                    {
                        CompoundQstOverGst = taxProfile.CompoundQstOverGst,
                        GstRate = taxProfile.GstRate,
                        HstRate = taxProfile.GstRate,
                        PstRate = taxProfile.GstRate,
                        QstRate = taxProfile.GstRate,
                    }
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading company for edit: {CompanyId}", id);
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CompanyEditViewModel model)
        {
            ViewData["Id"] = id;

            if (!ModelState.IsValid)
            {

                return View(model);
            }

            try
            {
                // ✅ Construct the Address ValueObject from the AddressViewModel
                var address = new Address(model.Address.Country,
                    model.Address.State,
                    model.Address.City,
                    model.Address.Street,
                    model.Address.ZipCode);


                // ✅ Construct the TaxProfile ValueObject from the TaxProfileViewModel
                var taxProfile = new TaxProfile(
                    model.TaxProfile.GstRate,
                    model.TaxProfile.QstRate,
                    model.TaxProfile.PstRate,
                    model.TaxProfile.HstRate,
                    model.TaxProfile.CompoundQstOverGst
                );


                var companyDto = new CompanyUpdateDto
                {
                    Name = model.Name,
                    RegistrationNumber = model.RegistrationNumber,
                    Address = MappingHelper.FormatAddress(address),
                    TaxProfile = MappingHelper.FormatTaxProfile(taxProfile)
                };

                await _companyService.UpdateCompanyAsync(id, companyDto);

                TempData["SuccessMessage"] = "Company updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company: {CompanyId}", id);
                ModelState.AddModelError("", "An error occurred while updating the company. Please try again.");
                return View(model);
            }
        }

        // GET: Admin/Company/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                return View(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading company for delete: {CompanyId}", id);
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var result = await _companyService.DeleteCompanyAsync(id);
                if (!result)
                {
                    return NotFound();
                }

                TempData["SuccessMessage"] = "Company deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company: {CompanyId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the company. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
