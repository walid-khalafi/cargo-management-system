using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.DriverBatch
{
    /// <summary>
    /// Represents a single wait time entry in a driver batch.
    /// </summary>
    /// <remarks>
    /// Used to record idle or waiting periods during a batch, such as loading delays,
    /// customs clearance, or mechanical downtime.  
    /// Contains hours waited, rate per hour, and total payment.
    /// </remarks>
    public class DriverBatchWaitDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the date of the wait time entry (in UTC).
        /// </summary>
        /// <example>2025-08-06T00:00:00Z</example>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the total hours of wait time.
        /// </summary>
        /// <example>3.5</example>
        public decimal Hours { get; set; }

        /// <summary>
        /// Gets or sets the category or type of wait (e.g., Loading, Customs, Breakdown).
        /// </summary>
        /// <example>Customs</example>
        public string WaitType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hourly rate paid for wait time.
        /// </summary>
        /// <example>20.00</example>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the total pay for this wait time entry (calculated as Hours × Rate).
        /// </summary>
        /// <example>70.00</example>
        public decimal Total { get; set; }

        /// <summary>
        /// Gets or sets an optional note or description providing context for the wait time.
        /// </summary>
        /// <example>Delayed at border checkpoint</example>
        public string Description { get; set; } = string.Empty;
    }

}
