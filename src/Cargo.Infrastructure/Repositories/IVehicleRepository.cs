using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository interface for Vehicle entity with specific operations.
/// </summary>
public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    /// <summary>
    /// Gets vehicles by company ID.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of vehicles for the company.</returns>
    Task<IReadOnlyList<Vehicle>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default);

    /// <summary>
    /// Gets active vehicles.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of active vehicles.</returns>
    Task<IReadOnlyList<Vehicle>> GetActiveVehiclesAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets vehicles by status.
    /// </summary>
    /// <param name="status">The vehicle status.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of vehicles with the specified status.</returns>
    Task<IReadOnlyList<Vehicle>> GetByStatusAsync(VehicleStatus status, CancellationToken ct = default);

    /// <summary>
    /// Gets vehicles by fuel type.
    /// </summary>
    /// <param name="fuelType">The fuel type.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of vehicles with the specified fuel type.</returns>
    Task<IReadOnlyList<Vehicle>> GetByFuelTypeAsync(string fuelType, CancellationToken ct = default);

    /// <summary>
    /// Gets vehicles by license plate number.
    /// </summary>
    /// <param name="licensePlate">The license plate number.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The vehicle with the specified license plate, or null if not found.</returns>
    Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken ct = default);
}
