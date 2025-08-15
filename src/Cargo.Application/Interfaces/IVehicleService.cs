using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Vehicles;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for vehicle-related business operations
    /// </summary>
    public interface IVehicleService
    {
        Task<VehicleDto> GetVehicleByIdAsync(Guid id);
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto> CreateVehicleAsync(VehicleCreateDto dto);
        Task<VehicleDto> UpdateVehicleAsync(Guid id, VehicleUpdateDto dto);
        Task<bool> DeleteVehicleAsync(Guid id);
        Task<IEnumerable<VehicleDto>> GetVehiclesByCompanyAsync(Guid companyId);
        Task<IEnumerable<VehicleDto>> GetAvailableVehiclesAsync();
        Task<IEnumerable<VehicleDto>> GetVehiclesByDriverAsync(Guid driverId);
    }
}
