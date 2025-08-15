using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for DriverBatchLoad entity with specific operations.
    /// </summary>
    public interface IDriverBatchLoadRepository : IGenericRepository<DriverBatchLoad>
    {
        /// <summary>
        /// Gets all load records for a specific driver batch.
        /// </summary>
        /// <param name="driverBatchId">The driver batch identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records for the batch.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByDriverBatchIdAsync(Guid driverBatchId, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records for a specific driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records for the driver.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByDriverIdAsync(Guid driverId, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records by load type.
        /// </summary>
        /// <param name="loadType">The load type.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records with the specified load type.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByLoadTypeAsync(LoadType loadType, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records by rate type.
        /// </summary>
        /// <param name="rateType">The rate type.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records with the specified rate type.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByRateTypeAsync(RateType rateType, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records by DAR number.
        /// </summary>
        /// <param name="darNumber">The DAR number.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records with the specified DAR number.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByDarNumberAsync(string darNumber, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records by load number.
        /// </summary>
        /// <param name="loadNumber">The load number.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records with the specified load number.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByLoadNumberAsync(string loadNumber, CancellationToken ct = default);

        /// <summary>
        /// Gets all load records within a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of load records within the date range.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);

        /// <summary>
        /// Gets the total pay for all loads in a driver batch.
        /// </summary>
        /// <param name="driverBatchId">The driver batch identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The total pay amount for all loads in the batch.</returns>
        Task<decimal> GetTotalPayByDriverBatchIdAsync(Guid driverBatchId, CancellationToken ct = default);

        /// <summary>
        /// Gets load records with pagination support.
        /// </summary>
        /// <param name="pageIndex">The page index (0-based).</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A paged list of load records.</returns>
        Task<IReadOnlyList<DriverBatchLoad>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct = default);
    }
}
