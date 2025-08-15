using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cargo.Domain.Interfaces;
using Cargo.Domain.Entities;
using AutoMapper;
using Cargo.Application.DTOs.Company;
using Cargo.Web.Areas.Admin.Models.CompanyViewModels;
using System.Linq;

namespace Cargo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
  //  [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CompanyController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Admin/Company
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string search = "")
        {
            try
            {
                var companies = await _unitOfWork.Companies.GetAllAsync();
                
                if (!string.IsNullOrWhiteSpace(search))
                {
                    companies = (IReadOnlyList<Company>)companies.Where(c => 
                        c.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase) ||
                        c.RegistrationNumber.Contains(search, System.StringComparison.OrdinalIgnoreCase));
                }

                var totalCount = companies.Count();
                var pagedCompanies = companies
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var model = new CompanyListViewModel
                {
                    Companies = _mapper.Map<List<CompanyDto>>(pagedCompanies),
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
                var company = await _unitOfWork.Companies.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<CompanyDto>(company);
                return View(model);
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

        // POST: Admin/Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var company = _mapper.Map<Company>(model);
                await _unitOfWork.Companies.AddAsync(company);
                await _unitOfWork.SaveChangesAsync();

                TempData["SuccessMessage"] = "Company created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                ModelState.AddModelError("", "An error occurred while creating the company. Please try again.");
                return View(model);
            }
        }

        // GET: Admin/Company/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var company = await _unitOfWork.Companies.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }
                ViewData["Id"] = id;
                var model = _mapper.Map<CompanyUpdateDto>(company);
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
        public async Task<IActionResult> Edit(Guid id, CompanyUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var company = await _unitOfWork.Companies.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                _mapper.Map(model, company);
                await _unitOfWork.Companies.UpdateAsync(company);
                await _unitOfWork.SaveChangesAsync();

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
                var company = await _unitOfWork.Companies.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<CompanyDto>(company);
                return View(model);
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
                var company = await _unitOfWork.Companies.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                await _unitOfWork.Companies.RemoveAsync(company);
                await _unitOfWork.SaveChangesAsync();

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
