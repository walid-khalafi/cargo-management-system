using System;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents an hourly entry in a driver batch statement.
    /// </summary>
    public class DriverBatchHourly : BaseEntity
    {
        /// <summary>
        /// Gets or sets the date of the hourly entry.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the number of hours worked.
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes worked.
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Gets or sets the rate per hour.
        /// </summary>
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Calculates the total pay for this hourly entry.
        /// </summary>
        /// <returns>The calculated pay.</returns>
        public decimal CalculatePay()
        {
            return (Hours + Minutes / 60m) * RatePerHour;
        }

        /// <summary>
        /// Gets or sets the foreign key for the associated driver batch.
        /// </summary>
        public Guid DriverBatchId { get; set; }

        /// <summary>
        /// Gets or sets the associated driver batch.
        /// </summary>
        public DriverBatch DriverBatch { get; set; }
    }
}
