using System;
using System.Collections.Generic;
using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.DriverBatch
{
    /// <summary>
    /// Represents a complete driver batch record including work entries, loads, and wait times.
    /// Inherits from <see cref="BaseDto"/> to include unique identifier and audit metadata.
    /// </summary>
    /// <remarks>
    /// A driver batch groups together all work activities (hourly work, loads, wait times)
    /// within a defined time period for payroll and reporting purposes.
    /// </remarks>
    public class DriverBatchDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the batch reference number.
        /// </summary>
        /// <example>BATCH-2025-08-001</example>
        public string BatchNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the batch (in UTC).
        /// </summary>
        /// <example>2025-08-01T00:00:00Z</example>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the batch (in UTC).
        /// </summary>
        /// <example>2025-08-07T23:59:59Z</example>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the current status of the batch (e.g., Pending, Approved, Paid).
        /// </summary>
        /// <example>Approved</example>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the driver for this batch.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid DriverId { get; set; }

        /// <summary>
        /// Gets or sets the full name of the driver.
        /// </summary>
        /// <example>John Doe</example>
        public string DriverName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total hours worked in this batch.
        /// </summary>
        /// <example>48.5</example>
        public decimal TotalHours { get; set; }

        /// <summary>
        /// Gets or sets the total miles driven in this batch.
        /// </summary>
        /// <example>1250.75</example>
        public decimal TotalMiles { get; set; }

        /// <summary>
        /// Gets or sets the total pay for this batch.
        /// </summary>
        /// <example>1850.00</example>
        public decimal TotalPay { get; set; }

        /// <summary>
        /// Gets or sets the list of hourly work entries.
        /// </summary>
        public List<DriverBatchHourlyDto> HourlyEntries { get; set; } = new List<DriverBatchHourlyDto>();

        /// <summary>
        /// Gets or sets the list of load entries.
        /// </summary>
        public List<DriverBatchLoadDto> LoadEntries { get; set; } = new List<DriverBatchLoadDto>();

        /// <summary>
        /// Gets or sets the list of wait time entries.
        /// </summary>
        public List<DriverBatchWaitDto> WaitEntries { get; set; } = new List<DriverBatchWaitDto>();
    }

}
