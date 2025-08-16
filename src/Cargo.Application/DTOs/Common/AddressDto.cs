using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.Common
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Street address is required")]
        [StringLength(200, ErrorMessage = "Street address cannot exceed 200 characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State/Province is required")]
        [StringLength(100, ErrorMessage = "State/Province cannot exceed 100 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "ZIP/Postal Code is required")]
        [StringLength(20, ErrorMessage = "ZIP/Postal Code cannot exceed 20 characters")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string Country { get; set; }
    }
}
