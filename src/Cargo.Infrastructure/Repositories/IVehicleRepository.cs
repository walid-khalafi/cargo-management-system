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

    #region VehicleOwnership Operations

    /// <summary>
    /// Gets vehicle ownership records for a specific vehicle.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of vehicle ownership records.</returns>
    Task<IReadOnlyList<VehicleOwnership>> GetVehicleOwnershipsAsync(Guid vehicleId, CancellationToken ct = default);

    /// <summary>
    /// Gets the current active ownership record for a vehicle.
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The current vehicle ownership record, or null if not found.</returns>
    Task<VehicleOwnership?> GetCurrentVehicleOwnershipAsync(Guid vehicleId, CancellationToken ct = default);

    /// <summary>
    /// Gets vehicles owned by a specific company.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of vehicles owned by the company.</returns>
    Task<IReadOnlyList<Vehicle>> GetVehiclesByCompanyOwnershipAsync(Guid companyId, CancellationToken ct = default);

    /// <summary>
    /// Gets active vehicle ownership records for a company.
    /// </summary>
    /// <param name="companyId">The company identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of active vehicle ownership records.</returns>
    Task<IReadOnlyList<VehicleOwnership>> GetActiveVehicleOwnershipsByCompanyAsync(Guid companyId, CancellationToken ct = default);

    /// <summary>
    /// Gets vehicle ownership records by ownership type.
    /// </summary>
    /// <param name="ownershipType">The ownership type.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of vehicle ownership records.</returns>
    Task<IReadOnlyList<VehicleOwnership>> GetVehicleOwnershipsByTypeAsync(OwnershipType ownershipType, CancellationToken ct = default);

    #endregion
}
