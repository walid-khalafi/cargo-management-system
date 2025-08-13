using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Common
{
    public class TaxProfileDto
    {
        public decimal GstRate { get; set; }
        public decimal QstRate { get; set; }
        public decimal PstRate { get; set; }
        public decimal HstRate { get; set; }
        public bool CompoundQstOverGst { get; set; }
    }
}
