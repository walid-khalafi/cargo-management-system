using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository interface for Route entity
/// </summary>
public interface IRouteRepository : IGenericRepository<Route>
{
    /// <summary>
    /// Gets all routes by status
    /// </summary>
    /// <param name="status">The route status</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of routes with the specified status</returns>
    Task<IReadOnlyList<Route>> GetByStatusAsync(RouteStatus status, CancellationToken ct = default);
    
    /// <summary>
    /// Gets all routes by type
    /// </summary>
    /// <param name="type">The route type</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of routes with the specified type</returns>
    Task<IReadOnlyList<Route>> GetByTypeAsync(RouteType type, CancellationToken ct = default);
    
    /// <summary>
    /// Gets all routes for a specific driver
    /// </summary>
    /// <param name="driverId">The driver identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of routes assigned to the driver</returns>
    Task<IReadOnlyList<Route>> GetByDriverIdAsync(Guid driverId, CancellationToken ct = default);
    
    /// <summary>
    /// Gets all routes for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of routes assigned to the vehicle</returns>
    Task<IReadOnlyList<Route>> GetByVehicleIdAsync(Guid vehicleId, CancellationToken ct = default);
}
