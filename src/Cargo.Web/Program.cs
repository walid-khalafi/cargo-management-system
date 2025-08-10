using Cargo.Infrastructure.Identity;
using Cargo.Infrastructure.Data;
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
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<CargoDbContext>()
                .AddDefaultTokenProviders();


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
            .AddEntityFrameworkStores<CargoDbContext>()
            .AddDefaultTokenProviders();

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
