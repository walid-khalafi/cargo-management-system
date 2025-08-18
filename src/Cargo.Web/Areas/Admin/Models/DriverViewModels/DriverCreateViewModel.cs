using System;
using System.ComponentModel.DataAnnotations;
using Cargo.Application.DTOs.Common;

namespace Cargo.Web.Areas.Admin.Models.DriverViewModels
{
    public class DriverCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address")]
        public AddressDto Address { get; set; } = new AddressDto();

        [Required]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }

        [Required]
        [Display(Name = "License Type")]
        public string LicenseType { get; set; }

        [Required]
        [Display(Name = "License Expiry Date")]
        public DateTime LicenseExpiryDate { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, 80)]
        [Display(Name = "Years of Experience")]
        public int YearsOfExperience { get; set; }

        [Required]
        [Display(Name = "Company")]
        public Guid CompanyId { get; set; }
    }
}
