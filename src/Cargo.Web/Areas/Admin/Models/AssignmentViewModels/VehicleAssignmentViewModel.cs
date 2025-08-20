using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Application.DTOs.Vehicles;

namespace Cargo.Web.Areas.Admin.Models.AssignmentViewModels
{
    // View Models
    public class VehicleAssignmentViewModel
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<VehicleDto> Vehicles { get; set; } = new();
        public List<DriverVehicleAssignmentDto> Assignments { get; set; } = new();
    }

   
}
