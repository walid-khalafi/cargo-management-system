using Microsoft.AspNetCore.Identity;

namespace Cargo.Infrastructure.Identity
{
    /// <summary>
    /// Represents an application user in the system that extends the ASP.NET Core Identity framework.
    /// This class contains additional user profile information beyond the base IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        /// <value>The first name of the user.</value>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        /// <value>The last name of the user.</value>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the user's full name by combining FirstName and LastName.
        /// This is a read-only computed property that automatically handles trimming.
        /// </summary>
        /// <value>The full name in the format "FirstName LastName".</value>
        /// <example>
        /// If FirstName = "John" and LastName = "Doe", FullName returns "John Doe"
        /// If FirstName = "John" and LastName is empty, FullName returns "John"
        /// </example>
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
