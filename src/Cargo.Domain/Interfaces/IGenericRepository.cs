using System.Linq.Expressions;

namespace Cargo.Domain.Interfaces
{

    /// <summary>
    /// Generic repository interface for basic CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The entity or null if not found.</returns>
        Task<T?> GetByIdAsync(object id, CancellationToken ct = default);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of all entities.</returns>
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);

        /// <summary>
        /// Finds entities matching the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A read-only list of matching entities.</returns>
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="ct">The cancellation token.</param>
        Task AddAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Adds multiple entities.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <param name="ct">The cancellation token.</param>
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="ct">The cancellation token.</param>
        Task UpdateAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Removes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <param name="ct">The cancellation token.</param>
        Task RemoveAsync(T entity, CancellationToken ct = default);

        /// <summary>
        /// Removes multiple entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        /// <param name="ct">The cancellation token.</param>
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}