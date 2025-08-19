using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Vehicles;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for vehicle-related business operations using UnitOfWork pattern
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(Guid id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto> CreateVehicleAsync(VehicleCreateDto dto)
        {
            var vehicle = _mapper.Map<Vehicle>(dto);
            await _unitOfWork.Vehicles.AddAsync(vehicle);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Create Vehicle : {ex}");
            }
           
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<VehicleDto> UpdateVehicleAsync(Guid id, VehicleUpdateDto dto)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
                throw new KeyNotFoundException($"Vehicle with ID {id} not found");

            _mapper.Map(dto, vehicle);
            await _unitOfWork.Vehicles.UpdateAsync(vehicle);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
          
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
                return false;

            await _unitOfWork.Vehicles.RemoveAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByCompanyAsync(Guid companyId)
        {
            var vehicles = await _unitOfWork.Vehicles.FindAsync(v => v.OwnerCompanyId == companyId);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<IEnumerable<VehicleDto>> GetAvailableVehiclesAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.FindAsync(v => v.IsAvailable);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByDriverAsync(Guid driverId)
        {
            // This would need to be implemented via driver-vehicle assignments
            // For now, return empty list as this requires additional repository methods
            return new List<VehicleDto>();
        }
    }
}
