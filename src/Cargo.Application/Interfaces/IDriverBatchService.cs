using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.DriverBatches;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for driver batch processing and payroll calculations
    /// </summary>
    public interface IDriverBatchService
    {
        Task<DriverBatchDto> GetDriverBatchByIdAsync(Guid id);
        Task<IEnumerable<DriverBatchDto>> GetDriverBatchesByDriverAsync(Guid driverId);
        Task<DriverBatchDto> CreateDriverBatchAsync(DriverBatchCreateDto dto);
        Task<DriverBatchDto> UpdateDriverBatchAsync(Guid id, DriverBatchUpdateDto dto);
        Task<bool> DeleteDriverBatchAsync(Guid id);
        Task<IEnumerable<DriverBatchDto>> GetDriverBatchesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<DriverBatchDto> ProcessDriverBatchAsync(Guid batchId);
        Task<decimal> CalculateDriverEarningsAsync(Guid driverId, DateTime startDate, DateTime endDate);
        Task<decimal> CalculateDriverRateAsync(Guid driverId, decimal baseRate);
    }
}
