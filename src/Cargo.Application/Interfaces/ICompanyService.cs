using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Company;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for company-related business operations
    /// </summary>
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompanyByIdAsync(Guid id);
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto dto);
        Task<CompanyDto> UpdateCompanyAsync(Guid id, CompanyUpdateDto dto);
        Task<bool> DeleteCompanyAsync(Guid id);
        Task<IEnumerable<CompanyDto>> GetCompaniesByDriverAsync(Guid driverId);
        Task<IEnumerable<CompanyDto>> GetCompaniesByVehicleAsync(Guid vehicleId);
    }
}
