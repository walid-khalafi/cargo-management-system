using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Identity;
using Cargo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Cargo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register ApplicationDbContext with SQL Server
            builder.Services.AddDbContext<CargoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Register Identity services


            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Username settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Password settings
                options.Password.RequireDigit = true;                // Requires at least one numeric digit
                options.Password.RequiredLength = 8;                 // Minimum password length
                options.Password.RequireNonAlphanumeric = true;      // Requires at least one special character
                options.Password.RequireUppercase = true;            // Requires at least one uppercase letter
                options.Password.RequireLowercase = true;            // Requires at least one lowercase letter
                options.Password.RequiredUniqueChars = 1;            // Requires at least one unique character

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Lockout duration
                options.Lockout.MaxFailedAccessAttempts = 5;                      // Max failed attempts before lockout
                options.Lockout.AllowedForNewUsers = true;                        // Enable lockout for new users

                // Sign-in settings
                options.SignIn.RequireConfirmedEmail = false;         // Email confirmation not required
                options.SignIn.RequireConfirmedPhoneNumber = false;   // Phone confirmation not required
            })
                .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<CargoDbContext>()
            .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
                options.Cookie.IsEssential = true;

            });
            builder.Services.AddDistributedMemoryCache();



            // Configure repository dependencies for Dependency Injection (DI) with Scoped lifetime.
            //
            // 1. Generic repository registration:
            //    - Maps IGenericRepository<TEntity> to GenericRepository<TEntity> for any entity type.
            //      This enables common CRUD operations across the domain without rewriting logic.
            //
            // 2. Specific repository registrations:
            //    - IVehicleRepository  -> VehicleRepository   // Vehicle entity data access
            //    - IDriverRepository   -> DriverRepository    // Driver entity data access
            //    - ICompanyRepository  -> CompanyRepository   // Company entity data access
            //    - IRouteRepository    -> RouteRepository     // Route entity data access
            //    - IApplicationUserRepository    -> ApplicationUserRepository     // ApplicationUser entity data access
            //    - IApplicationRoleRepository    -> ApplicationRoleRepository     // ApplicationRole entity data access
            //
            // Scoped lifetime ensures each HTTP request gets its own repository instances,
            // which works in harmony with the EF Core DbContext lifetime to avoid concurrency issues.
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();   // Redirect HTTP requests to HTTPS
            app.UseRouting();            // Enable routing


            // Add authentication middleware
            app.UseAuthentication();     // Enable authentication middleware
            app.UseAuthorization();      // Enable authorization middleware

            // Map static assets and default route
            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run(); // Start the application
        }
    }
}
