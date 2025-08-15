using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for managing driver-vehicle assignments using UnitOfWork pattern
    /// </summary>
    public class DriverVehicleAssignmentService : IDriverVehicleAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DriverVehicleAssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DriverVehicleAssignmentDto> GetAssignmentByIdAsync(Guid id)
        {
            var assignment = await _unitOfWork.Repository<DriverVehicleAssignment>().GetByIdAsync(id);
            return _mapper.Map<DriverVehicleAssignmentDto>(assignment);
        }

        public async Task<IEnumerable<DriverVehicleAssignmentDto>> GetAllAssignmentsAsync()
        {
            var assignments = await _unitOfWork.Repository<DriverVehicleAssignment>().GetAllAsync();
            return _mapper.Map<IEnumerable<DriverVehicleAssignmentDto>>(assignments);
        }

        public async Task<DriverVehicleAssignmentDto> CreateAssignmentAsync(CreateDriverVehicleAssignmentDto dto)
        {
            var assignment = _mapper.Map<DriverVehicleAssignment>(dto);
            await _unitOfWork.Repository<DriverVehicleAssignment>().AddAsync(assignment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverVehicleAssignmentDto>(assignment);
        }

        public async Task<DriverVehicleAssignmentDto> UpdateAssignmentAsync(Guid id, UpdateDriverVehicleAssignmentDto dto)
        {
            var assignment = await _unitOfWork.Repository<DriverVehicleAssignment>().GetByIdAsync(id);
            if (assignment == null)
                throw new KeyNotFoundException($"Assignment with ID {id} not found");

            _mapper.Map(dto, assignment);
            await _unitOfWork.Repository<DriverVehicleAssignment>().UpdateAsync(assignment);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverVehicleAssignmentDto>(assignment);
        }

        public async Task<bool> DeleteAssignmentAsync(Guid id)
        {
            var assignment = await _unitOfWork.Repository<DriverVehicleAssignment>().GetByIdAsync(id);
            if (assignment == null)
                return false;

            await _unitOfWork.Repository<DriverVehicleAssignment>().RemoveAsync(assignment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DriverVehicleAssignmentDto>> GetAssignmentsByDriverAsync(Guid driverId)
        {
            var assignments = await _unitOfWork.Repository<DriverVehicleAssignment>()
                .FindAsync(a => a.DriverId == driverId);
            return _mapper.Map<IEnumerable<DriverVehicleAssignmentDto>>(assignments);
        }

        public async Task<IEnumerable<DriverVehicleAssignmentDto>> GetAssignmentsByVehicleAsync(Guid vehicleId)
        {
            var assignments = await _unitOfWork.Repository<DriverVehicleAssignment>()
                .FindAsync(a => a.VehicleId == vehicleId);
            return _mapper.Map<IEnumerable<DriverVehicleAssignmentDto>>(assignments);
        }

        public async Task<IEnumerable<DriverVehicleAssignmentDto>> GetActiveAssignmentsAsync()
        {
            var assignments = await _unitOfWork.Repository<DriverVehicleAssignment>()
                .FindAsync(a => a.Status == AssignmentStatus.Active);
            return _mapper.Map<IEnumerable<DriverVehicleAssignmentDto>>(assignments);
        }

        public async Task<IEnumerable<DriverVehicleAssignmentDto>> GetAssignmentsByStatusAsync(AssignmentStatus status)
        {
            var assignments = await _unitOfWork.Repository<DriverVehicleAssignment>()
                .FindAsync(a => a.Status == status);
            return _mapper.Map<IEnumerable<DriverVehicleAssignmentDto>>(assignments);
        }

        public async Task<bool> EndAssignmentAsync(Guid id, string reason)
        {
            var assignment = await _unitOfWork.Repository<DriverVehicleAssignment>().GetByIdAsync(id);
            if (assignment == null)
                return false;

            // Update the assignment status and end details
            // This would need to be implemented based on the actual entity structure
            await _unitOfWork.Repository<DriverVehicleAssignment>().UpdateAsync(assignment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
