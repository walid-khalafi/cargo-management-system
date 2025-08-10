using Microsoft.AspNetCore.Identity;

namespace Cargo.Infrastructure.Identity
{
    /// <summary>
    /// Represents an application role in the system that extends the ASP.NET Core Identity framework.
    /// This class contains additional role information beyond the base IdentityRole.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// Gets or sets the description of the role.
        /// </summary>
        /// <value>A brief description explaining the purpose and permissions of this role.</value>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name for the role.
        /// </summary>
        /// <value>A user-friendly display name for the role that can be shown in UI elements.</value>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the ApplicationRole class.
        /// </summary>
        public ApplicationRole() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ApplicationRole class with the specified role name.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        public ApplicationRole(string roleName) : base(roleName)
        {
            DisplayName = roleName;
        }
    }
}
