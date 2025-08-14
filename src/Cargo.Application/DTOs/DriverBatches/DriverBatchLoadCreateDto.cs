using Cargo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for creating a new DriverBatchLoad entry.
    /// </summary>
    public class DriverBatchLoadCreateDto
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

        // Pay components that come from invoice or dispatch
        public decimal FscPay { get; set; }
        public decimal TemporaryEmergencyFuelPay { get; set; }

        // Relationship
        public Guid DriverBatchId { get; set; }
    }
}


