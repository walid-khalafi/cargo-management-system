using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Application.DTOs.DriverContracts;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for driver contract management and rate configuration using UnitOfWork pattern
    /// </summary>
    public class DriverContractService : IDriverContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DriverContractService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DriverContractDto> GetDriverContractByIdAsync(Guid id)
        {
            var contract = await _unitOfWork.DriverContracts.GetByIdAsync(id);
            return _mapper.Map<DriverContractDto>(contract);
        }

        public async Task<IEnumerable<DriverContractDto>> GetDriverContractsByDriverAsync(Guid driverId)
        {
            var contracts = await _unitOfWork.DriverContracts.FindAsync(c => c.DriverId == driverId);
            return _mapper.Map<IEnumerable<DriverContractDto>>(contracts);
        }

        public async Task<DriverContractDto> GetActiveDriverContractAsync(Guid driverId)
        {
            var contracts = await _unitOfWork.DriverContracts.FindAsync(c => 
                c.DriverId == driverId && 
                c.StartDate <= DateTime.UtcNow && 
                (!c.EndDate.HasValue || c.EndDate.Value >= DateTime.UtcNow));
            
            return _mapper.Map<DriverContractDto>(contracts.FirstOrDefault());
        }

        public async Task<DriverContractDto> CreateDriverContractAsync(DriverContractCreateDto dto)
        {
            var contract = _mapper.Map<DriverContract>(dto);
            await _unitOfWork.DriverContracts.AddAsync(contract);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverContractDto>(contract);
        }

        public async Task<DriverContractDto> UpdateDriverContractAsync(Guid id, DriverContractUpdateDto dto)
        {
            var contract = await _unitOfWork.DriverContracts.GetByIdAsync(id);
            if (contract == null)
                throw new KeyNotFoundException($"Driver contract with ID {id} not found");

            _mapper.Map(dto, contract);
            await _unitOfWork.DriverContracts.UpdateAsync(contract);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverContractDto>(contract);
        }

        public async Task<bool> DeleteDriverContractAsync(Guid id)
        {
            var contract = await _unitOfWork.DriverContracts.GetByIdAsync(id);
            if (contract == null)
                return false;

            await _unitOfWork.DriverContracts.RemoveAsync(contract);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DriverContractDto>> GetActiveContractsAsync()
        {
            var contracts = await _unitOfWork.DriverContracts.FindAsync(c => 
                c.StartDate <= DateTime.UtcNow && 
                (!c.EndDate.HasValue || c.EndDate.Value >= DateTime.UtcNow));
            
            return _mapper.Map<IEnumerable<DriverContractDto>>(contracts);
        }

        public async Task<IEnumerable<DriverContractDto>> GetContractsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var contracts = await _unitOfWork.DriverContracts.FindAsync(c => 
                c.StartDate <= endDate && 
                (!c.EndDate.HasValue || c.EndDate.Value >= startDate));
            
            return _mapper.Map<IEnumerable<DriverContractDto>>(contracts);
        }
    }
}
