using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.DriverBatches
{
    using System;

    namespace Cargo.Application.DTOs
    {
        /// <summary>
        /// DTO for creating a new DriverBatchHourly entry.
        /// </summary>
        public class DriverBatchHourlyCreateDto
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
            /// Optional exact pay value from invoice.
            /// </summary>
            public decimal? TotalPayFromInvoice { get; set; }

            /// <summary>
            /// Foreign key for the related DriverBatch.
            /// </summary>
            public Guid DriverBatchId { get; set; }
        }
    }

}


