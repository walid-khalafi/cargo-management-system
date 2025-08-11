using Microsoft.EntityFrameworkCore;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Infrastructure.Data;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Vehicle entity with specific operations.
/// </summary>
public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    private readonly CargoDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public VehicleRepository(CargoDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Vehicle>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(v => v.OwnerCompanyId == companyId)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Vehicle>> GetActiveVehiclesAsync(CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(v => v.Status == VehicleStatus.Available && v.IsAvailable)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Vehicle>> GetByStatusAsync(VehicleStatus status, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(v => v.Status == status)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Vehicle>> GetByFuelTypeAsync(string fuelType, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .Where(v => v.FuelType == fuelType)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken ct = default)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.PlateNumber.Value == licensePlate, ct);
    }
}
