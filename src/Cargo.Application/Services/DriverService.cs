using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Driver;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for driver-related business operations using UnitOfWork pattern
    /// </summary>
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DriverDto> GetDriverByIdAsync(Guid id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            return _mapper.Map<DriverDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
        {
            var drivers = await _unitOfWork.Drivers.GetAllAsync();
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<DriverDto> CreateDriverAsync(DriverCreateDto dto)
        {
            var driver = _mapper.Map<Driver>(dto);
            await _unitOfWork.Drivers.AddAsync(driver);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverDto>(driver);
        }

        public async Task<DriverDto> UpdateDriverAsync(Guid id, DriverUpdateDto dto)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (driver == null)
                throw new KeyNotFoundException($"Driver with ID {id} not found");

            _mapper.Map(dto, driver);
            await _unitOfWork.Drivers.UpdateAsync(driver);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverDto>(driver);
        }

        public async Task<bool> DeleteDriverAsync(Guid id)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(id);
            if (driver == null)
                return false;

            await _unitOfWork.Drivers.RemoveAsync(driver);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DriverDto>> GetDriversByCompanyAsync(Guid companyId)
        {
            var drivers = await _unitOfWork.Drivers.FindAsync(d => d.CompanyId == companyId);
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<IEnumerable<DriverDto>> GetActiveDriversAsync()
        {
            var drivers = await _unitOfWork.Drivers.FindAsync(d => d.Status == DriverStatus.Active);
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<IEnumerable<DriverDto>> GetDriversByStatusAsync(DriverStatus status)
        {
            var drivers = await _unitOfWork.Drivers.FindAsync(d => d.Status == status);
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }
    }
}
