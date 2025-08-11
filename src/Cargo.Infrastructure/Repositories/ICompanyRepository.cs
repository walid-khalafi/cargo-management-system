using Cargo.Domain.Entities;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository interface for Company entity
/// </summary>
public interface ICompanyRepository : IGenericRepository<Company>
{
    /// <summary>
    /// Gets a company by its registration number
    /// </summary>
    /// <param name="registrationNumber">The company registration number</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>The company if found</returns>
    Task<Company?> GetByRegistrationNumberAsync(string registrationNumber, CancellationToken ct = default);
    
    /// <summary>
    /// Gets companies by name (partial match)
    /// </summary>
    /// <param name="name">The company name to search for</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of matching companies</returns>
    Task<IReadOnlyList<Company>> GetCompaniesByNameAsync(string name, CancellationToken ct = default);
}
