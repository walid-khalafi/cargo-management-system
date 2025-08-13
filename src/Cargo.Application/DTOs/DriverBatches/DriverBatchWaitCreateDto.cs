using Cargo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for creating a new DriverBatchWait entry.
    /// </summary>
    public class DriverBatchWaitCreateDto
    {
        // Identifiers
        public string DarNumber { get; set; }
        public string CpPoNumber { get; set; }

        // Wait details
        public WaitType WaitType { get; set; }
        public int WaitMinutes { get; set; }
        public decimal RatePerMinute { get; set; }
        public decimal Multiplier { get; set; } = 1.0m;

        // Optional invoice values
        public decimal? RawPayFromInvoice { get; set; }
        public decimal? FinalPayFromInvoice { get; set; }

        // Foreign Key
        public Guid DriverBatchId { get; set; }
    }
}


