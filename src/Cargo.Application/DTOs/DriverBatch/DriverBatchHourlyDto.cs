using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.DriverBatch
{
    /// <summary>
    /// Represents a single hourly work entry within a driver batch.
    /// </summary>
    /// <remarks>
    /// Each hourly entry contains the work date, total hours worked, 
    /// pay rate, calculated total pay, and an optional description.
    /// Used for detailed payroll and activity reporting.
    /// </remarks>
    public class DriverBatchHourlyDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the date when the work was performed (in UTC).
        /// </summary>
        /// <example>2025-08-05T00:00:00Z</example>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the total number of hours worked on the specified date.
        /// </summary>
        /// <example>8.5</example>
        public decimal HoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the hourly pay rate for the work performed.
        /// </summary>
        /// <example>25.00</example>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the total pay for this entry (calculated as HoursWorked × Rate).
        /// </summary>
        /// <example>212.50</example>
        public decimal Total { get; set; }

        /// <summary>
        /// Gets or sets an optional note or description for the work performed.
        /// May include context like “Overtime” or “Night Shift”.
        /// </summary>
        /// <example>Overtime driving shift</example>
        public string Description { get; set; } = string.Empty;
    }

}
