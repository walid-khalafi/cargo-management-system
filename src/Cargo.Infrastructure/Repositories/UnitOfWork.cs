using Cargo.Domain.Entities;
using Cargo.Domain.Interfaces;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Infrastructure.Repositories
{
    /// <summary>
    /// Unit of Work implementation for managing transactions across multiple repositories.
    /// Supports both sync and async disposal for test/runtime flexibility.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly CargoDbContext _context;

        // Repositories injected to ensure they share the same DbContext instance
        public IApplicationUserRepository ApplicationUsers { get; }
        public IApplicationRoleRepository ApplicationRoles { get; }
        public ICompanyRepository Companies { get; }
        public IDriverRepository Drivers { get; }
        public IDriverContractRepository DriverContracts { get; }
        public IRouteRepository Routes { get; }
        public IVehicleRepository Vehicles { get; }

        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(
           CargoDbContext context,
           IApplicationUserRepository applicationUsers,
           IApplicationRoleRepository applicationRoles,
           ICompanyRepository companies,
           IDriverRepository drivers,
           IDriverContractRepository driverContracts,
           IRouteRepository routes,
           IVehicleRepository vehicles)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ApplicationUsers = applicationUsers ?? throw new ArgumentNullException(nameof(applicationUsers));
            ApplicationRoles = applicationRoles ?? throw new ArgumentNullException(nameof(applicationRoles));
            Companies = companies ?? throw new ArgumentNullException(nameof(companies));
            Drivers = drivers ?? throw new ArgumentNullException(nameof(drivers));
            DriverContracts = driverContracts ?? throw new ArgumentNullException(nameof(driverContracts));
            Routes = routes ?? throw new ArgumentNullException(nameof(routes));
            Vehicles = vehicles ?? throw new ArgumentNullException(nameof(vehicles));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is not null) return;
            _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException(
                    "No active transaction to commit. Call BeginTransactionAsync() first on the same UnitOfWork instance."
                );

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await _currentTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }


        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException("No active transaction to roll back.");

            try
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
        public async Task ExecuteInTransactionAsync(
           Func<CancellationToken, Task> action,
           CancellationToken cancellationToken = default)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));

            await BeginTransactionAsync(cancellationToken);
            try
            {
                await action(cancellationToken);
                await CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
            => new GenericRepository<TEntity>(_context);

        // Sync dispose for tests/frameworks that expect IDisposable
        public void Dispose()
        {
            DisposeAsync().AsTask().GetAwaiter().GetResult();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }

            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}