using Cargo.Application.Interfaces;
using Cargo.Application.Services;
using Cargo.Domain.Interfaces;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Identity;
using Cargo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Cargo.Application.Mapping;
namespace Cargo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // DbContext registration
            builder.Services.AddDbContext<CargoDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Identity services
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<CargoDbContext>()
            .AddDefaultTokenProviders();

            // Cookie settings
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDistributedMemoryCache();

            // AutoMapper registration
            builder.Services.AddAutoMapper(typeof(CompanyMappingProfile).Assembly);

            // Repository registrations
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<IDriverContractRepository, DriverContractRepository>();
            builder.Services.AddScoped<IDriverBatchLoadRepository, DriverBatchLoadRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();

            // Service registrations
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IDriverBatchService, DriverBatchService>();
            builder.Services.AddScoped<IDriverContractService, DriverContractService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IDriverVehicleAssignmentService, DriverVehicleAssignmentService>();
            builder.Services.AddScoped<IRouteService, RouteService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();

            // UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
            }
            else
            {
                builder.Services.AddControllersWithViews();
            }


            var app = builder.Build();



            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "admin",
                areaName: "Admin",
                pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

            // Routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            app.Run();
        }
    }
}