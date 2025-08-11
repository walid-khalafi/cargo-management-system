using Microsoft.EntityFrameworkCore;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Infrastructure.Data;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Driver entity with specific operations.
/// </summary>
public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    private readonly CargoDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DriverRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DriverRepository(CargoDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Driver>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default)
    {
        return await _context.Drivers
            .Where(d => d.CompanyId == companyId)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Driver>> GetActiveDriversAsync(CancellationToken ct = default)
    {
        return await _context.Drivers
            .Where(d => d.Status == DriverStatus.Active)
            .ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Driver>> GetByStatusAsync(DriverStatus status, CancellationToken ct = default)
    {
        return await _context.Drivers
            .Where(d => d.Status == status)
            .ToListAsync(ct);
    }
}
