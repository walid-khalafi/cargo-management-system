using System;
using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for creating a new DriverBatch.
    /// Loads/Waits/Hourlies are added via their own endpoints.
    /// </summary>
    public class DriverBatchCreateDto
    {
        // Identity & period
        public string BatchNumber { get; set; }
        public Guid DriverId { get; set; }
        public DateTime StatementStartDate { get; set; }
        public DateTime StatementEndDate { get; set; }

        // Ownership snapshot
        public Guid? VehicleOwnershipId { get; set; }
        public OwnershipType OwnershipTypeAtBatch { get; set; } = OwnershipType.OwnedByFleet;

        // Payouts & fees
        public decimal WaitingPayoutPercentage { get; set; }
        public decimal DriverSharePercentage { get; set; }
        public decimal AdminFeePercent { get; set; } = 0m;
        public decimal AdminFeeFlat { get; set; } = 0m;
        public bool AdminFeeAppliesBeforeTaxes { get; set; } = false;

        // Adjustments (optional initial)
        public decimal AdjustmentsTotal { get; set; } = 0m;

        // Tax profile snapshot (required)
        public TaxProfileDto TaxProfile { get; set; }
    }
}
