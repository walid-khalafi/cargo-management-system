using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a wait entry in a driver batch statement, matching PDF/Excel data for audit.
    /// </summary>
    public class DriverBatchWait : BaseEntity
    {
        // Identifiers
        public string DarNumber { get; private set; }
        public string CpPoNumber { get; private set; }

        // Wait details
        public WaitType WaitType { get; private set; }
        public int WaitMinutes { get; private set; }
        public decimal RatePerMinute { get; private set; }
        public decimal Multiplier { get; private set; } = 1.0m;

        // Snapshot pay breakdown
        /// <summary>
        /// Raw wait pay before applying the batch's payout percentage.
        /// </summary>
        public decimal RawPay { get; private set; }

        /// <summary>
        /// Final pay after applying the batch's payout percentage (WaitingTotal contribution).
        /// Stored if imported; otherwise computed dynamically by DriverBatch.
        /// </summary>
        public decimal FinalPay { get; private set; }

        // FK
        public Guid DriverBatchId { get; private set; }
        public virtual DriverBatch DriverBatch { get; private set; }

        private DriverBatchWait() { } // EF

        public DriverBatchWait(
            string darNumber,
            string cpPoNumber,
            WaitType waitType,
            int waitMinutes,
            decimal ratePerMinute,
            decimal multiplier = 1.0m,
            decimal? rawPayFromInvoice = null,
            decimal? finalPayFromInvoice = null)
        {
            if (waitMinutes < 0) throw new ArgumentOutOfRangeException(nameof(waitMinutes));
            if (ratePerMinute < 0) throw new ArgumentOutOfRangeException(nameof(ratePerMinute));
            if (multiplier <= 0) throw new ArgumentOutOfRangeException(nameof(multiplier));

            DarNumber = darNumber;
            CpPoNumber = cpPoNumber;
            WaitType = waitType;
            WaitMinutes = waitMinutes;
            RatePerMinute = ratePerMinute;
            Multiplier = multiplier;

            RawPay = rawPayFromInvoice.HasValue
                ? Round2(rawPayFromInvoice.Value)
                : Round2(WaitMinutes * RatePerMinute * Multiplier);

            FinalPay = finalPayFromInvoice.HasValue
                ? Round2(finalPayFromInvoice.Value)
                : RawPay; // Will be adjusted by DriverBatch.WaitingPayoutPercentage
        }

        /// <summary>
        /// Recalculates raw pay (before payout %) based on minutes, rate, and multiplier.
        /// </summary>
        public decimal CalculateRawPay() => RawPay;

        private static decimal Round2(decimal value) =>
            Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}