using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Driver;
using Cargo.Domain.Enums;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for driver-related business operations
    /// </summary>
    public interface IDriverService
    {
        Task<DriverDto> GetDriverByIdAsync(Guid id);
        Task<IEnumerable<DriverDto>> GetAllDriversAsync();
        Task<DriverDto> CreateDriverAsync(DriverCreateDto dto);
        Task<DriverDto> UpdateDriverAsync(Guid id, DriverUpdateDto dto);
        Task<bool> DeleteDriverAsync(Guid id);
        Task<IEnumerable<DriverDto>> GetDriversByCompanyAsync(Guid companyId);
        Task<IEnumerable<DriverDto>> GetActiveDriversAsync();
        Task<IEnumerable<DriverDto>> GetDriversByStatusAsync(DriverStatus status);
    }
}
