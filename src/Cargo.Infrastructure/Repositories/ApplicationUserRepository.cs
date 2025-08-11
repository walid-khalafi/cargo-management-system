using Cargo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for ApplicationUser operations.
/// </summary>
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUserRepository"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="roleManager">The role manager.</param>
    public ApplicationUserRepository(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    /// <inheritdoc />
    public async Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await _userManager.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<ApplicationUser?> GetByUserNameAsync(string userName, CancellationToken ct = default)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    /// <inheritdoc />
    public async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync(CancellationToken ct = default)
    {
        var users = await _userManager.Users.ToListAsync(ct);
        return users.AsReadOnly();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ApplicationUser>> FindAsync(Func<ApplicationUser, bool> predicate, CancellationToken ct = default)
    {
        var users = _userManager.Users.Where(predicate).ToList();
        return await Task.FromResult(users.AsReadOnly());
    }

    /// <inheritdoc />
    public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password, CancellationToken ct = default)
    {
        return await _userManager.CreateAsync(user, password);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken ct = default)
    {
        return await _userManager.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken ct = default)
    {
        return await _userManager.DeleteAsync(user);
    }

    /// <inheritdoc />
    public async Task<bool> UserExistsAsync(string userName, CancellationToken ct = default)
    {
        return await _userManager.FindByNameAsync(userName) != null;
    }

    /// <inheritdoc />
    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken ct = default)
    {
        var users = await _userManager.GetUsersInRoleAsync(roleName);
        return users.AsReadOnly();
    }
}
