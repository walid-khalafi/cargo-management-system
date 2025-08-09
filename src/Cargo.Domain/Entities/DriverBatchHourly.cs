using System;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents an hourly pay entry in a driver batch statement,
    /// aligned with PDF/Excel snapshot data for audit accuracy.
    /// </summary>
    public class DriverBatchHourly : BaseEntity
    {
        /// <summary>
        /// Date this hourly work was performed.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Hours worked (whole hours).
        /// </summary>
        public int Hours { get; private set; }

        /// <summary>
        /// Additional minutes worked (0–59).
        /// </summary>
        public int Minutes { get; private set; }

        /// <summary>
        /// Rate per hour at the time of the batch (snapshot value).
        /// </summary>
        public decimal RatePerHour { get; private set; }

        /// <summary>
        /// Total pay for this entry (can be from invoice or calculated).
        /// </summary>
        public decimal TotalPay { get; private set; }

        // FK
        public Guid DriverBatchId { get; private set; }
        public virtual DriverBatch DriverBatch { get; private set; }

        private DriverBatchHourly() { } // EF Core

        /// <summary>
        /// Creates an hourly entry.
        /// </summary>
        /// <param name="date">Work date.</param>
        /// <param name="hours">Whole hours worked.</param>
        /// <param name="minutes">Additional minutes (0-59).</param>
        /// <param name="ratePerHour">Hourly rate.</param>
        /// <param name="totalPayFromInvoice">If imported, the exact pay value from invoice; optional.</param>
        public DriverBatchHourly(
            DateTime date,
            int hours,
            int minutes,
            decimal ratePerHour,
            decimal? totalPayFromInvoice = null)
        {
            if (hours < 0) throw new ArgumentOutOfRangeException(nameof(hours));
            if (minutes < 0 || minutes > 59) throw new ArgumentOutOfRangeException(nameof(minutes));
            if (ratePerHour < 0) throw new ArgumentOutOfRangeException(nameof(ratePerHour));

            Date = date;
            Hours = hours;
            Minutes = minutes;
            RatePerHour = ratePerHour;

            TotalPay = totalPayFromInvoice.HasValue
                ? Round2(totalPayFromInvoice.Value)
                : CalculatePay();
        }

        /// <summary>
        /// Calculates the pay based on hours, minutes, and rate.
        /// </summary>
        public decimal CalculatePay()
        {
            var totalHours = Hours + (Minutes / 60m);
            return Round2(totalHours * RatePerHour);
        }

        private static decimal Round2(decimal value) =>
            Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}