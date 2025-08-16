using Cargo.Application.DTOs.Company;
using System.Collections.Generic;

namespace Cargo.Web.Areas.Admin.Models.CompanyViewModels
{
    public class CompanyListViewModel
    {
        public List<CompanyDto> Companies { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public int TotalCount { get; set; }
    }
}
