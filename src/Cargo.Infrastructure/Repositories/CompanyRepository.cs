using Cargo.Domain.Entities;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Cargo.Domain.Interfaces;
namespace Cargo.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Company entity
    /// </summary>
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly CargoDbContext _context;

        public CompanyRepository(CargoDbContext context) : base(context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Company?> GetByRegistrationNumberAsync(string registrationNumber, CancellationToken ct = default)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.RegistrationNumber == registrationNumber, ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Company>> GetCompaniesByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Companies
                .Where(c => c.Name.Contains(name))
                .OrderBy(c => c.Name)
                .ToListAsync(ct);
        }
    }
}


