using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a wait entry in a driver batch statement.
    /// </summary>
    public class DriverBatchWait : BaseEntity
    {
        /// <summary>
        /// Gets or sets the DAR number.
        /// </summary>
        public string DarNumber { get; set; }

        /// <summary>
        /// Gets or sets the CP PO number.
        /// </summary>
        public string CpPoNumber { get; set; }

        /// <summary>
        /// Gets or sets the wait type.
        /// </summary>
        public WaitType WaitType { get; set; }

        /// <summary>
        /// Gets or sets the wait time in minutes.
        /// </summary>
        public int WaitMinutes { get; set; }

        /// <summary>
        /// Gets or sets the rate per minute.
        /// </summary>
        public decimal RatePerMinute { get; set; }

        /// <summary>
        /// Gets or sets the multiplier for the wait pay calculation.
        /// </summary>
        public decimal Multiplier { get; set; } = 1.0m;

        /// <summary>
        /// Gets or sets the wait pay amount.
        /// </summary>
        public decimal WaitPay { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated driver batch.
        /// </summary>
        public Guid DriverBatchId { get; set; }

        /// <summary>
        /// Gets or sets the associated driver batch.
        /// </summary>
        public DriverBatch DriverBatch { get; set; }

        /// <summary>
        /// Calculates the raw pay for this wait entry.
        /// </summary>
        /// <returns>The calculated raw pay.</returns>
        public decimal CalculateRawPay()
        {
            return WaitMinutes * RatePerMinute * Multiplier;
        }
    }
}
