using Cargo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for ApplicationRole operations.
/// </summary>
public class ApplicationRoleRepository : IApplicationRoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationRoleRepository"/> class.
    /// </summary>
    /// <param name="roleManager">The role manager.</param>
    public ApplicationRoleRepository(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    /// <inheritdoc />
    public async Task<ApplicationRole?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _roleManager.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<ApplicationRole?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return await _roleManager.FindByNameAsync(name);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ApplicationRole>> GetAllAsync(CancellationToken ct = default)
    {
        var roles = await _roleManager.Roles.ToListAsync(ct);
        return roles.AsReadOnly();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ApplicationRole>> FindAsync(Func<ApplicationRole, bool> predicate, CancellationToken ct = default)
    {
        var roles = _roleManager.Roles.Where(predicate).ToList();
        return await Task.FromResult(roles.AsReadOnly());
    }

    /// <inheritdoc />
    public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken ct = default)
    {
        return await _roleManager.CreateAsync(role);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken ct = default)
    {
        return await _roleManager.UpdateAsync(role);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken ct = default)
    {
        return await _roleManager.DeleteAsync(role);
    }

    /// <inheritdoc />
    public async Task<bool> RoleExistsAsync(string name, CancellationToken ct = default)
    {
        return await _roleManager.RoleExistsAsync(name);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<string>> GetRoleClaimsAsync(string roleId, CancellationToken ct = default)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            return new List<string>().AsReadOnly();

        var claims = await _roleManager.GetClaimsAsync(role);
        return claims.Select(c => c.Value).ToList().AsReadOnly();
    }

    /// <inheritdoc />
    public async Task<IdentityResult> AddClaimAsync(ApplicationRole role, string claimType, string claimValue, CancellationToken ct = default)
    {
        return await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claimType, claimValue));
    }

    /// <inheritdoc />
    public async Task<IdentityResult> RemoveClaimAsync(ApplicationRole role, string claimType, string claimValue, CancellationToken ct = default)
    {
        return await _roleManager.RemoveClaimAsync(role, new System.Security.Claims.Claim(claimType, claimValue));
    }
}
