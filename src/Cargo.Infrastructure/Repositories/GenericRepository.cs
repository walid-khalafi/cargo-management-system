using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Cargo.Infrastructure.Data;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation for basic CRUD operations using EF Core.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly CargoDbContext _context;
    private readonly DbSet<T> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GenericRepository(CargoDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
    }

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(object id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync(new[] { id }, cancellationToken: ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbSet.ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate, 
        CancellationToken ct = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        await _dbSet.AddRangeAsync(entities, ct);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task RemoveAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        _dbSet.RemoveRange(entities);
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}
