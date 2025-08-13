using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.DriverContracts
{
    public class RateBandDto
    {
        public string Label { get; set; }
        public decimal Rate { get; set; }
        public decimal? MileageThreshold { get; set; }
    }
}
