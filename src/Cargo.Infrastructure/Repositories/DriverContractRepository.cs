using Cargo.Domain.Entities;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for DriverContract entity
/// </summary>
public class DriverContractRepository : GenericRepository<DriverContract>, IDriverContractRepository
{
    private readonly CargoDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DriverContractRepository"/> class.
    /// </summary>
    /// <param name="context">The database context</param>
    public DriverContractRepository(CargoDbContext context) : base(context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<DriverContract>> GetByDriverIdAsync(Guid driverId, CancellationToken ct = default)
    {
        return await _context.DriverContracts
            .Include(dc => dc.Driver)
            .Where(dc => dc.DriverId == driverId)
            .OrderByDescending(dc => dc.StartDate)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<DriverContract>> GetActiveContractsAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        return await _context.DriverContracts
            .Include(dc => dc.Driver)
            .Where(dc => dc.StartDate <= now && (dc.EndDate == null || dc.EndDate >= now))
            .OrderBy(dc => dc.DriverId)
            .ThenByDescending(dc => dc.StartDate)
            .ToListAsync(ct);
    }
}
