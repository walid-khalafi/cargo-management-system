using Cargo.Domain.Entities;
using Cargo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Data;

public class CargoDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public CargoDbContext(DbContextOptions<CargoDbContext> options)
        : base(options)
    {
    }


    // Aggregate Roots
    public DbSet<Company> Companies { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Route> Routes { get; set; }
    
    // Related entities
    public DbSet<DriverBatch> DriverBatches { get; set; }
    public DbSet<DriverBatchHourly> DriverBatchHourlies { get; set; }
    public DbSet<DriverBatchLoad> DriverBatchLoads { get; set; }
    public DbSet<DriverBatchWait> DriverBatchWaits { get; set; }
    public DbSet<DriverContract> DriverContracts { get; set; }
    public DbSet<DriverVehicleAssignment> DriverVehicleAssignments { get; set; }
    public DbSet<VehicleOwnership> VehicleOwnerships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CargoDbContext).Assembly);
    }
}
