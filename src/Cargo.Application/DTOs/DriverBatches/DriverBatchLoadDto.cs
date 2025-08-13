using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for transferring DriverBatchLoad data without domain logic.
    /// </summary>
    public class DriverBatchLoadDto : BaseEntityDto
    {
        // Invoice & Dispatch Identifiers
        public string DarNumber { get; set; }
        public string LoadNumber { get; set; }

        // Routing Information
        public string OriginPc { get; set; }
        public string DestinationPc { get; set; }

        // Mileage & Load Type
        public int LegMiles { get; set; }
        public LoadType LoadType { get; set; }
        public RateType RateType { get; set; }

        // Rate Details
        public decimal Rate { get; set; }
        public string BandLabel { get; set; }

        // Pay Breakdown
        public decimal BasePay { get; set; }
        public decimal FscPay { get; set; }
        public decimal TemporaryEmergencyFuelPay { get; set; }
        public decimal NetWefp { get; set; }

        // Relationships
        public Guid DriverBatchId { get; set; }
    }
}


