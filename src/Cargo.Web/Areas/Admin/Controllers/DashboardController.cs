using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cargo.Domain.Interfaces;
using Cargo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cargo.Web.Areas.Admin.Models;

namespace Cargo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUnitOfWork unitOfWork,
            ILogger<DashboardController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalUsers = await _userManager.Users.CountAsync(),
                TotalRoles = await _roleManager.Roles.CountAsync(),
                TotalCompanies =  _unitOfWork.Companies.GetAllAsync().Result.Count,
                TotalDrivers = _unitOfWork.Drivers.GetAllAsync().Result.Count()
            };

            return View(model);
        }
    }
}
