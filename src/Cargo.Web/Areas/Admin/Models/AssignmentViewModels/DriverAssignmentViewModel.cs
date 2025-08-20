using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Application.DTOs.Driver;

namespace Cargo.Web.Areas.Admin.Models.AssignmentViewModels
{
    public class DriverAssignmentViewModel
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<DriverDto> Drivers { get; set; } = new();
        public List<DriverVehicleAssignmentDto> Assignments { get; set; } = new();
    }

   
}
