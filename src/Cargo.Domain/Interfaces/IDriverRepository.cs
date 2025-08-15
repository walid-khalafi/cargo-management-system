using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Interfaces
{

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

        /// <summary>
        /// Gets all contracts for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of contracts for the driver.</returns>
        Task<IReadOnlyList<DriverContract>> GetDriverContractsAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all vehicle assignments for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of vehicle assignments for the driver.</returns>
        Task<IReadOnlyList<DriverVehicleAssignment>> GetDriverVehicleAssignmentsAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all active vehicle assignments for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of active vehicle assignments for the driver.</returns>
        Task<IReadOnlyList<DriverVehicleAssignment>> GetActiveDriverVehicleAssignmentsAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all batches for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of batches for the driver.</returns>
        Task<IReadOnlyList<DriverBatch>> GetDriverBatchesAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all hourly records for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of hourly records for the driver.</returns>
        Task<IReadOnlyList<DriverBatchHourly>> GetDriverBatchHourliesAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records for the driver.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetDriverBatchLoadsAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all wait records for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of wait records for the driver.</returns>
        Task<IReadOnlyList<DriverBatchWait>> GetDriverBatchWaitsAsync(Guid driverId, CancellationToken ct = default);
    }
}