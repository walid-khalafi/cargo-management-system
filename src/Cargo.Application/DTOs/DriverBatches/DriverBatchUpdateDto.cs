using System;
using Cargo.Domain.Enums;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for updating an existing DriverBatch.
    /// Only mutable fields are included; status transitions are optional.
    /// </summary>
    public class DriverBatchUpdateDto
    {
        public Guid Id { get; set; }

        // Period (optional changes)
        public DateTime? StatementStartDate { get; set; }
        public DateTime? StatementEndDate { get; set; }

        // Ownership snapshot
        public Guid? VehicleOwnershipId { get; set; }
        public OwnershipType? OwnershipTypeAtBatch { get; set; }

        // Payouts & fees
        public decimal? WaitingPayoutPercentage { get; set; }
        public decimal? DriverSharePercentage { get; set; }
        public decimal? AdminFeePercent { get; set; }
        public decimal? AdminFeeFlat { get; set; }
        public bool? AdminFeeAppliesBeforeTaxes { get; set; }

        // Adjustments
        public decimal? AdjustmentsTotal { get; set; }

        // Status change
        public BatchStatus? Status { get; set; }

        // Tax profile snapshot replacement
        public TaxProfileDto? TaxProfile { get; set; }
    }
}
