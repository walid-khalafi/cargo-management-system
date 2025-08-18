using Cargo.Application.DTOs.Driver;
using System.Collections.Generic;

namespace Cargo.Web.Areas.Admin.Models.DriverViewModels
{
    public class DriverListViewModel
    {
        public List<DriverDto> Drivers { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; } = "";
        public int TotalCount { get; set; }
    }
}
