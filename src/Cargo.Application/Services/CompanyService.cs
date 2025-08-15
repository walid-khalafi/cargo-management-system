using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Company;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for company-related business operations using UnitOfWork pattern
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(Guid id, CompanyUpdateDto dto)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null)
                throw new KeyNotFoundException($"Company with ID {id} not found");

            _mapper.Map(dto, company);
            await _unitOfWork.Companies.UpdateAsync(company);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null)
                return false;

            await _unitOfWork.Companies.RemoveAsync(company);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByDriverAsync(Guid driverId)
        {
            // This would need to be implemented via a query or join
            // For now, return empty list as this requires additional repository methods
            return new List<CompanyDto>();
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByVehicleAsync(Guid vehicleId)
        {
            // This would need to be implemented via a query or join
            // For now, return empty list as this requires additional repository methods
            return new List<CompanyDto>();
        }
    }
}
