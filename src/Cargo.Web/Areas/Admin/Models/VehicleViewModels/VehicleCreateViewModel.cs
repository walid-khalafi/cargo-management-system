using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.VehicleViewModels
{
    public class VehicleCreateViewModel
    {
        [Required(ErrorMessage = "Make is required")]
        [StringLength(100, ErrorMessage = "Make cannot exceed 100 characters")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(100, ErrorMessage = "Model cannot exceed 100 characters")]
        public string VehicleModel { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1980, 2100, ErrorMessage = "Year must be between 1980 and 2100")]
        public int Year { get; set; }

        [StringLength(50, ErrorMessage = "Color cannot exceed 50 characters")]
        public string Color { get; set; } 

        [Required(ErrorMessage = "VIN is required")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be exactly 17 characters")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "VIN must contain only valid characters (A-Z except I, O, Q, and 0-9)")]
        public string VIN { get; set; } 

        [Required(ErrorMessage = "Registration number is required")]
        [StringLength(50, ErrorMessage = "Registration number cannot exceed 50 characters")]
        public string RegistrationNumber { get; set; } 

        [Required(ErrorMessage = "Plate number details are required")]
        public PlateNumberViewModel PlateNumber { get; set; }

        [Required(ErrorMessage = "Fuel type is required")]
        [StringLength(50, ErrorMessage = "Fuel type cannot exceed 50 characters")]
        public string FuelType { get; set; } 

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Owner company is required")]
        public Guid OwnerCompanyId { get; set; }
    }
}
