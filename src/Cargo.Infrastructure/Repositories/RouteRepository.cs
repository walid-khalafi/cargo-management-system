using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Route entity
/// </summary>
public class RouteRepository : GenericRepository<Route>, IRouteRepository
{
    private readonly CargoDbContext _context;

    public RouteRepository(CargoDbContext context) : base(context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Route>> GetByStatusAsync(RouteStatus status, CancellationToken ct = default)
    {
        return await _context.Routes
            .Where(r => r.Status == status)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Route>> GetByTypeAsync(RouteType type, CancellationToken ct = default)
    {
        return await _context.Routes
            .Where(r => r.RouteType == type)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Route>> GetByDriverIdAsync(Guid driverId, CancellationToken ct = default)
    {
        return await _context.Routes
            .Include(r => r.AssignedDrivers)
            .Where(r => r.AssignedDrivers.Any(d => d.Id == driverId))
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Route>> GetByVehicleIdAsync(Guid vehicleId, CancellationToken ct = default)
    {
        return await _context.Routes
            .Include(r => r.AssignedVehicles)
            .Where(r => r.AssignedVehicles.Any(v => v.Id == vehicleId))
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(ct);
    }
}
