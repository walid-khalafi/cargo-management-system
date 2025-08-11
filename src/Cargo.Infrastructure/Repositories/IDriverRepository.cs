using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository interface for Driver entity with specific operations.
/// </summary>
public interface IDriverRepository : IGenericRepository<Driver>
{
    /// <summary>
    /// Gets drivers by company ID.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of drivers for the company.</returns>
    Task<IReadOnlyList<Driver>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default);

    /// <summary>
    /// Gets active drivers.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of active drivers.</returns>
    Task<IReadOnlyList<Driver>> GetActiveDriversAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets drivers by status.
    /// </summary>
    /// <param name="status">The driver status.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of drivers with the specified status.</returns>
    Task<IReadOnlyList<Driver>> GetByStatusAsync(DriverStatus status, CancellationToken ct = default);
}
