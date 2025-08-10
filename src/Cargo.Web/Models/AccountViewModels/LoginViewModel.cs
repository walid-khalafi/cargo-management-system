using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Models.AccountViewModels
{
    /// <summary>
    /// View model for user login functionality
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the username for authentication
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for authentication
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to persist the login session
        /// </summary>
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
