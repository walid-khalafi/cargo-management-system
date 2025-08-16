using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Application.DTOs.DriverBatches;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for driver batch processing and payroll calculations using UnitOfWork pattern
    /// </summary>
    public class DriverBatchService : IDriverBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DriverBatchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DriverBatchDto> GetDriverBatchByIdAsync(Guid id)
        {
            var batch = await _unitOfWork.Repository<DriverBatch>().GetByIdAsync(id);
            return _mapper.Map<DriverBatchDto>(batch);
        }

        public async Task<IEnumerable<DriverBatchDto>> GetDriverBatchesByDriverAsync(Guid driverId)
        {
            var batches = await _unitOfWork.Repository<DriverBatch>().FindAsync(b => b.DriverId == driverId);
            return _mapper.Map<IEnumerable<DriverBatchDto>>(batches);
        }

        public async Task<DriverBatchDto> CreateDriverBatchAsync(DriverBatchCreateDto dto)
        {
            var batch = _mapper.Map<DriverBatch>(dto);
            await _unitOfWork.Repository<DriverBatch>().AddAsync(batch);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverBatchDto>(batch);
        }

        public async Task<DriverBatchDto> UpdateDriverBatchAsync(Guid id, DriverBatchUpdateDto dto)
        {
            var batch = await _unitOfWork.Repository<DriverBatch>().GetByIdAsync(id);
            if (batch == null)
                throw new KeyNotFoundException($"Driver batch with ID {id} not found");

            _mapper.Map(dto, batch);
            await _unitOfWork.Repository<DriverBatch>().UpdateAsync(batch);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DriverBatchDto>(batch);
        }

        public async Task<bool> DeleteDriverBatchAsync(Guid id)
        {
            var batch = await _unitOfWork.Repository<DriverBatch>().GetByIdAsync(id);
            if (batch == null)
                return false;

            await _unitOfWork.Repository<DriverBatch>().RemoveAsync(batch);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DriverBatchDto>> GetDriverBatchesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var batches = await _unitOfWork.Repository<DriverBatch>().FindAsync(b => 
                b.CreatedAt >= startDate && b.CreatedAt <= endDate);
            return _mapper.Map<IEnumerable<DriverBatchDto>>(batches);
        }

        public async Task<DriverBatchDto> ProcessDriverBatchAsync(Guid batchId)
        {
            var batch = await _unitOfWork.Repository<DriverBatch>().GetByIdAsync(batchId);
            return _mapper.Map<DriverBatchDto>(batch);
        }

        public async Task<decimal> CalculateDriverEarningsAsync(Guid driverId, DateTime startDate, DateTime endDate)
        {
            var batches = await _unitOfWork.Repository<DriverBatch>().FindAsync(b => 
                b.DriverId == driverId && b.CreatedAt >= startDate && b.CreatedAt <= endDate);
            
            // Calculate earnings based on batch data
            // This is a placeholder - actual calculation would depend on business rules
            return 0; // Placeholder return value
        }

        public async Task<decimal> CalculateDriverRateAsync(Guid driverId, decimal baseRate)
        {
            // Calculate driver rate based on experience, performance, etc.
            // This is a placeholder - actual calculation would depend on business rules
            return baseRate;
        }
    }
}
