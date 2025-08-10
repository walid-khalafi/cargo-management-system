using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Models.AccountViewModels
{
    /// <summary>
    /// View model for password reset functionality
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email address associated with the account
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new password for the account
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation for the new password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password reset token/code
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
