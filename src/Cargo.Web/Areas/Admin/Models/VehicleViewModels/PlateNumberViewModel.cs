using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.VehicleViewModels
{
    public class PlateNumberViewModel
    {
        [Required(ErrorMessage = "Issuing authority is required")]
        [StringLength(50, ErrorMessage = "Issuing authority cannot exceed 50 characters")]
        public string IssuingAuthority { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plate value is required")]
        [StringLength(20, ErrorMessage = "Plate value cannot exceed 20 characters")]
        public string Value { get; set; } = string.Empty;

        [Required(ErrorMessage = "Plate type is required")]
        [StringLength(30, ErrorMessage = "Plate type cannot exceed 30 characters")]
        public string PlateType { get; set; } = string.Empty;
    }
}
