using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.CommonViewModels
{
    /// <summary>
    /// ViewModel used to transfer address data between the UI and the application layer.
    /// This is mutable so that the ASP.NET Core Model Binder can populate it easily.
    /// </summary>
    public class AddressViewModel
    {
        /// <summary>
        /// Street name and number of the address.
        /// </summary>
        [Required(ErrorMessage = "Street is required.")]
        [StringLength(200, ErrorMessage = "Street cannot exceed 200 characters.")]
        public string Street { get; set; }

        /// <summary>
        /// City or locality.
        /// </summary>
        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        /// <summary>
        /// State, province, or region.
        /// </summary>
        [StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
        public string State { get; set; }

        /// <summary>
        /// Postal or ZIP code.
        /// </summary>
        [Required(ErrorMessage = "ZIP/Postal Code is required.")]
        [StringLength(20, ErrorMessage = "ZIP/Postal Code cannot exceed 20 characters.")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; }
    }
}