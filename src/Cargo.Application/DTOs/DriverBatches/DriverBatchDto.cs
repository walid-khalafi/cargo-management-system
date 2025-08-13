using System;
using System.Collections.Generic;
using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for transferring complete DriverBatch data (read model).
    /// </summary>
    public class DriverBatchDto : BaseEntityDto
    {
        // Identity & period
        public string BatchNumber { get; set; }
        public Guid DriverId { get; set; }
        public string? DriverName { get; set; }
        public DateTime StatementStartDate { get; set; }
        public DateTime StatementEndDate { get; set; }

        // Ownership snapshot
        public Guid? VehicleOwnershipId { get; set; }
        public OwnershipType OwnershipTypeAtBatch { get; set; }

        // Dray components
        public decimal DrayPay { get; set; }
        public decimal DrayFsc { get; set; }
        public decimal TemporaryEmergencyFsc { get; set; }
        public decimal DrayTotal { get; set; }

        // Payouts & fees
        public decimal WaitingPayoutPercentage { get; set; }
        public decimal DriverSharePercentage { get; set; }
        public decimal AdminFeePercent { get; set; }
        public decimal AdminFeeFlat { get; set; }
        public bool AdminFeeAppliesBeforeTaxes { get; set; }

        // Adjustments
        public decimal AdjustmentsTotal { get; set; }

        // Tax profile snapshot
        public TaxProfileDto TaxProfile { get; set; }

        // Collections
        public List<DriverBatchLoadDto> Loads { get; set; } = new();
        public List<DriverBatchWaitDto> Waits { get; set; } = new();
        public List<DriverBatchHourlyDto> Hourlies { get; set; } = new();

        // Totals
        public decimal TripTotal { get; set; }
        public decimal WaitingRawTotal { get; set; }
        public decimal WaitingTotal { get; set; }
        public decimal HourlyTotal { get; set; }
        public decimal WaitPay { get; set; }
        public decimal GrossRevenue { get; set; }

        // Share + taxes
        public decimal DriverShareAmount { get; set; }
        public TaxAmountsDto Taxes { get; set; }
        public decimal NetPay { get; set; }
        public decimal TotalPay { get; set; }

        // Status
        public BatchStatus Status { get; set; }
    }
}
