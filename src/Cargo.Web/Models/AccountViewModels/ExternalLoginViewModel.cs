using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Models.AccountViewModels
{
    /// <summary>
    /// View model for external login provider integration
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        /// Gets or sets the email address for external login association
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }
}
