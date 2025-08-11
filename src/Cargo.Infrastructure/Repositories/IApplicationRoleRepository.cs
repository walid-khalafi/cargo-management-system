using Cargo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository interface for ApplicationRole operations.
/// </summary>
public interface IApplicationRoleRepository
{
    /// <summary>
    /// Gets a role by its identifier.
    /// </summary>
    /// <param name="id">The role identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The role or null if not found.</returns>
    Task<ApplicationRole?> GetByIdAsync(string id, CancellationToken ct = default);

    /// <summary>
    /// Gets a role by its name.
    /// </summary>
    /// <param name="name">The role name.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The role or null if not found.</returns>
    Task<ApplicationRole?> GetByNameAsync(string name, CancellationToken ct = default);

    /// <summary>
    /// Gets all roles.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of all roles.</returns>
    Task<IReadOnlyList<ApplicationRole>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Finds roles matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter roles.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of matching roles.</returns>
    Task<IReadOnlyList<ApplicationRole>> FindAsync(Func<ApplicationRole, bool> predicate, CancellationToken ct = default);

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="role">The role to create.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The identity result of the creation operation.</returns>
    Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    /// <param name="role">The role to update.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The identity result of the update operation.</returns>
    Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken ct = default);

    /// <summary>
    /// Deletes a role.
    /// </summary>
    /// <param name="role">The role to delete.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The identity result of the deletion operation.</returns>
    Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken ct = default);

    /// <summary>
    /// Checks if a role exists with the specified name.
    /// </summary>
    /// <param name="name">The role name to check.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>True if the role exists; otherwise, false.</returns>
    Task<bool> RoleExistsAsync(string name, CancellationToken ct = default);

    /// <summary>
    /// Gets all permissions/claims for a specific role.
    /// </summary>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A read-only list of claims for the specified role.</returns>
    Task<IReadOnlyList<string>> GetRoleClaimsAsync(string roleId, CancellationToken ct = default);

    /// <summary>
    /// Adds a claim to a role.
    /// </summary>
    /// <param name="role">The role to add the claim to.</param>
    /// <param name="claimType">The claim type.</param>
    /// <param name="claimValue">The claim value.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The identity result of the operation.</returns>
    Task<IdentityResult> AddClaimAsync(ApplicationRole role, string claimType, string claimValue, CancellationToken ct = default);

    /// <summary>
    /// Removes a claim from a role.
    /// </summary>
    /// <param name="role">The role to remove the claim from.</param>
    /// <param name="claimType">The claim type.</param>
    /// <param name="claimValue">The claim value.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The identity result of the operation.</returns>
    Task<IdentityResult> RemoveClaimAsync(ApplicationRole role, string claimType, string claimValue, CancellationToken ct = default);
}
