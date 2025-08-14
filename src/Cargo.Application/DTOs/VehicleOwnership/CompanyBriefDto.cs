using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.VehicleOwnership
{
    public class CompanyBriefDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

}
