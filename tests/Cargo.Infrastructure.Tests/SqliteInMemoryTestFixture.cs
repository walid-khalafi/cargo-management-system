using Cargo.Domain.Interfaces;
using Cargo.Infrastructure.Data;
using Cargo.Infrastructure.Identity;
using Cargo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Cargo.Infrastructure.Tests
{
    /// <summary>
    /// Creates an in-memory SQLite database with all services registered,
    /// including ASP.NET Core Identity, repositories, and UnitOfWork.
    /// Designed for integration/unit testing.
    /// </summary>
    public sealed class SqliteInMemoryTestFixture : IAsyncDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; } = default!;
        public SqliteConnection Connection { get; private set; } = default!;

        public async Task InitializeAsync(Action<IServiceCollection>? extraRegistrations = null)
        {
            Connection = new SqliteConnection("Data Source=:memory:");
            await Connection.OpenAsync();

            var services = new ServiceCollection();

            services.AddDbContext<CargoDbContext>(options =>
            {
                options.UseSqlite(Connection);
            });

            // ✅ Register Identity so UserManager/RoleManager are available
            services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<CargoDbContext>();
                //.AddDefaultTokenProviders();

            // ✅ Register repositories and UnitOfWork
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDriverContractRepository, DriverContractRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Allow caller to override registrations
            extraRegistrations?.Invoke(services);

            ServiceProvider = services.BuildServiceProvider();

            // Ensure DB schema
            await using var scope = ServiceProvider.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<CargoDbContext>();
            await db.Database.EnsureCreatedAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (ServiceProvider is IAsyncDisposable asyncDisp)
                await asyncDisp.DisposeAsync();
            else if (ServiceProvider is IDisposable disp)
                disp.Dispose();

            if (Connection is not null)
                await Connection.DisposeAsync();
        }
    }
}