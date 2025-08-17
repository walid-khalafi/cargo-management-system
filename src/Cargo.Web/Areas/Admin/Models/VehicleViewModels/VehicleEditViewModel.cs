using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.VehicleViewModels
{
    public class VehicleEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Make { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string VehicleModel { get; set; } = string.Empty;

        [Range(1980, 2100)]
        public int Year { get; set; }

        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string VIN { get; set; } = string.Empty;

        [StringLength(50)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required]
        public PlateNumberViewModel PlateNumber { get; set; } = new PlateNumberViewModel();

        [StringLength(50)]
        public string FuelType { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        [Required]
        public Guid OwnerCompanyId { get; set; }
    }
}
