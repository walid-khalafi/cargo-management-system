using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.DriverContracts;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for driver contract management and rate configuration
    /// </summary>
    public interface IDriverContractService
    {
        Task<DriverContractDto> GetDriverContractByIdAsync(Guid id);
        Task<IEnumerable<DriverContractDto>> GetDriverContractsByDriverAsync(Guid driverId);
        Task<DriverContractDto> GetActiveDriverContractAsync(Guid driverId);
        Task<DriverContractDto> CreateDriverContractAsync(DriverContractCreateDto dto);
        Task<DriverContractDto> UpdateDriverContractAsync(Guid id, DriverContractUpdateDto dto);
        Task<bool> DeleteDriverContractAsync(Guid id);
        Task<IEnumerable<DriverContractDto>> GetActiveContractsAsync();
        Task<IEnumerable<DriverContractDto>> GetContractsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
