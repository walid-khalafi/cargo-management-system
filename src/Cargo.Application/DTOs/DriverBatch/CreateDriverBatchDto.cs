namespace Cargo.Application.DTOs.DriverBatch
{
    /// <summary>
    /// Represents the input required to create a new driver batch.
    /// </summary>
    /// <remarks>
    /// Used in HTTP POST requests to initialize a batch before adding work entries.
    /// Typically contains the minimal metadata needed to create the batch record
    /// before associating hourly, load, or wait entries.
    /// </remarks>
    public class CreateDriverBatchDto
    {
        /// <summary>
        /// Gets or sets the unique reference number assigned to the batch.
        /// This helps in tracking and correlating payroll records.
        /// </summary>
        /// <example>BATCH-2025-08-002</example>
        public string BatchNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the batch period in UTC.
        /// All work entries will be associated with dates between the start and end.
        /// </summary>
        /// <example>2025-08-08T00:00:00Z</example>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the batch period in UTC.
        /// This marks the last date on which work entries can be recorded for this batch.
        /// </summary>
        /// <example>2025-08-14T23:59:59Z</example>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the driver for whom this batch is created.
        /// Links the batch to the corresponding driver profile.
        /// </summary>
        /// <example>2dcd3b66-88bb-47d3-b98c-401c643c16c1</example>
        public Guid DriverId { get; set; }
    }

}
