using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for transferring DriverBatchWait data without domain logic.
    /// </summary>
    public class DriverBatchWaitDto  : BaseEntityDto
    {
        // Identifiers
        public string DarNumber { get; set; }
        public string CpPoNumber { get; set; }

        // Wait details
        public WaitType WaitType { get; set; }
        public int WaitMinutes { get; set; }
        public decimal RatePerMinute { get; set; }
        public decimal Multiplier { get; set; }

        // Snapshot pay breakdown
        public decimal RawPay { get; set; }
        public decimal FinalPay { get; set; }

        // Foreign Key
        public Guid DriverBatchId { get; set; }
    }
}


