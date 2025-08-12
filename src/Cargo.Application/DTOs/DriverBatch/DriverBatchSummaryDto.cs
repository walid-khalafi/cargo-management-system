using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.DriverBatch
{
    /// <summary>
    /// Represents summarized information for a driver batch.
    /// </summary>
    /// <remarks>
    /// Optimized for list or overview displays, showing only high-level metrics.
    /// Typically used in batch listings, dashboards, or payroll overviews.
    /// </remarks>
    public class DriverBatchSummaryDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the unique reference number assigned to the driver batch.
        /// Useful for tracking and cross-referencing batches in payroll records.
        /// </summary>
        /// <example>BATCH-2025-08-005</example>
        public string BatchNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the batch period in UTC.
        /// Defines the first date included in this summary's reporting range.
        /// </summary>
        /// <example>2025-08-01T00:00:00Z</example>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the batch period in UTC.
        /// Defines the last date included in this summary's reporting range.
        /// </summary>
        /// <example>2025-08-07T23:59:59Z</example>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the processing status of the batch.
        /// Common values include Pending, Approved, and Paid.
        /// </summary>
        /// <example>Approved</example>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full name of the driver associated with this batch.
        /// </summary>
        /// <example>John Doe</example>
        public string DriverName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total hours worked by the driver during this batch period.
        /// </summary>
        /// <example>42.5</example>
        public decimal TotalHours { get; set; }

        /// <summary>
        /// Gets or sets the total pay earned by the driver for this batch period.
        /// </summary>
        /// <example>1520.75</example>
        public decimal TotalPay { get; set; }
    }

}
