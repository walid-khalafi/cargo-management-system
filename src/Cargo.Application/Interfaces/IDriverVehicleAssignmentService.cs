using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Domain.Enums;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for managing driver-vehicle assignments
    /// </summary>
    public interface IDriverVehicleAssignmentService
    {
        Task<DriverVehicleAssignmentDto> GetAssignmentByIdAsync(Guid id);
        Task<IEnumerable<DriverVehicleAssignmentDto>> GetAllAssignmentsAsync();
        Task<DriverVehicleAssignmentDto> CreateAssignmentAsync(CreateDriverVehicleAssignmentDto dto);
        Task<DriverVehicleAssignmentDto> UpdateAssignmentAsync(Guid id, UpdateDriverVehicleAssignmentDto dto);
        Task<bool> DeleteAssignmentAsync(Guid id);
        Task<IEnumerable<DriverVehicleAssignmentDto>> GetAssignmentsByDriverAsync(Guid driverId);
        Task<IEnumerable<DriverVehicleAssignmentDto>> GetAssignmentsByVehicleAsync(Guid vehicleId);
        Task<IEnumerable<DriverVehicleAssignmentDto>> GetActiveAssignmentsAsync();
        Task<IEnumerable<DriverVehicleAssignmentDto>> GetAssignmentsByStatusAsync(AssignmentStatus status);
        Task<bool> EndAssignmentAsync(Guid id, string reason);
    }
}
