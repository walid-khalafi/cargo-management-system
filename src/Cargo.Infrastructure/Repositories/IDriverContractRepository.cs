using Cargo.Domain.Entities;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository interface for DriverContract entity
/// </summary>
public interface IDriverContractRepository : IGenericRepository<DriverContract>
{
    /// <summary>
    /// Gets all contracts for a specific driver
    /// </summary>
    /// <param name="driverId">The driver identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of contracts for the driver</returns>
    Task<IReadOnlyList<DriverContract>> GetByDriverIdAsync(Guid driverId, CancellationToken ct = default);
    
    /// <summary>
    /// Gets all active contracts
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of active contracts</returns>
    Task<IReadOnlyList<DriverContract>> GetActiveContractsAsync(CancellationToken ct = default);
}
