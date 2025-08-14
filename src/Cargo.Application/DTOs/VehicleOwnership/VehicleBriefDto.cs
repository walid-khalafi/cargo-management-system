using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.VehicleOwnership
{
    public class VehicleBriefDto
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string PlateNumber { get; set; } = default!;
    }

}
