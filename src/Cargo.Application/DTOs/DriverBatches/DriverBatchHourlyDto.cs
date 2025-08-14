using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for transferring DriverBatchHourly data without domain logic.
    /// </summary>
    public class DriverBatchHourlyDto : BaseEntityDto
    {
        /// <summary>
        /// Date this hourly work was performed.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Hours worked (whole hours).
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Additional minutes worked (0–59).
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Rate per hour at the time of the batch.
        /// </summary>
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Total pay for this entry.
        /// </summary>
        public decimal TotalPay { get; set; }

        /// <summary>
        /// Foreign key for the related DriverBatch.
        /// </summary>
        public Guid DriverBatchId { get; set; }
    }
}


