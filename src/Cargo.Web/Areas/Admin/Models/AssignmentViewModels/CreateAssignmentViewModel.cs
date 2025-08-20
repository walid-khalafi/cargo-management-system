using Cargo.Application.DTOs.Driver;
using Cargo.Application.DTOs.Vehicles;
using Cargo.Domain.Enums;

namespace Cargo.Web.Areas.Admin.Models.AssignmentViewModels
{
    public class CreateAssignmentViewModel
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<VehicleDto> Vehicles { get; set; } = new();
        public List<DriverDto> Drivers { get; set; } = new();
        public Guid SelectedVehicleId { get; set; }
        public Guid SelectedDriverId { get; set; }
        public DriverRoleType DriverRole { get; set; }
        public string Notes { get; set; }
    }
}
