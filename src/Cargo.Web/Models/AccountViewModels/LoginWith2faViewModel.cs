using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Models.AccountViewModels
{
    /// <summary>
    /// View model for two-factor authentication login
    /// </summary>
    public class LoginWith2faViewModel
    {
        /// <summary>
        /// Gets or sets the two-factor authentication code
        /// </summary>
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator Code")]
        public string TwoFactorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to remember this machine for future logins
        /// </summary>
        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember the user for the current session
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
