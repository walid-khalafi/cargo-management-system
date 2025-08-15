using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Domain.Interfaces
{
    /// <summary>
    /// Coordinates repositories with a shared persistence context and transactional boundaries.
    /// Keep EF-specific types out of Domain to preserve independence.
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        // Repositories (all should share the same DbContext instance in Infrastructure)
        IApplicationUserRepository ApplicationUsers { get; }
        IApplicationRoleRepository ApplicationRoles { get; }
        ICompanyRepository Companies { get; }
        IDriverRepository Drivers { get; }
        IDriverContractRepository DriverContracts { get; }
        IRouteRepository Routes { get; }
        IVehicleRepository Vehicles { get; }

        /// <summary>
        /// Optional generic access. Implemented in Infrastructure if you have a GenericRepository.
        /// </summary>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// Persists pending changes atomically.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Explicit transaction controls for multi-aggregate operations.
        /// </summary>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Convenience helper to execute a unit of work in a transaction.
        /// Rolls back on exception and rethrows.
        /// </summary>
        Task ExecuteInTransactionAsync(
            Func<CancellationToken, Task> action,
            CancellationToken cancellationToken = default);
    }
}