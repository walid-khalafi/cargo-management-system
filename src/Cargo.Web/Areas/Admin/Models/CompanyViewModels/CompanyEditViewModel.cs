using Cargo.Domain.ValueObjects;
using Cargo.Web.Areas.Admin.Models.CommonViewModels;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.CompanyViewModels
{
    public class CompanyEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Registration Number is required")]
        [StringLength(50, ErrorMessage = "Registration Number cannot exceed 50 characters")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public AddressViewModel Address { get; set; }

        [Required(ErrorMessage = "Tax Profile is required")]
        public TaxProfileViewModel TaxProfile { get; set; } 
    }
}