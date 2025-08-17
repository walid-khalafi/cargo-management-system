using Cargo.Application.DTOs.Vehicles;

namespace Cargo.Web.Areas.Admin.Models.VehicleViewModels
{
    public class VehicleListViewModel
    {
        public List<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public int TotalCount { get; set; }
    }
}
