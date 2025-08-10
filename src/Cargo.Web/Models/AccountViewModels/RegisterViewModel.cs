using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Models.AccountViewModels
{
    /// <summary>
    /// View model for user registration functionality
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the user's first name
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the email address for the user account
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number for the user
        /// </summary>
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user account
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password to validate against the original password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
