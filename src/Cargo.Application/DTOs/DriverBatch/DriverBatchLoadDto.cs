using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.DriverBatch
{
    /// <summary>
    /// Represents a single load entry in a driver batch, including mileage and rate.
    /// </summary>
    /// <remarks>
    /// Used to record a specific cargo load completed by a driver during the batch period.
    /// Contains mileage, rate per mile, and total earnings for the load.
    /// </remarks>
    public class DriverBatchLoadDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the date the load was transported (in UTC).
        /// </summary>
        /// <example>2025-08-06T00:00:00Z</example>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier or tracking number of the load.
        /// </summary>
        /// <example>LOAD-45892</example>
        public string LoadNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the origin location of the load.
        /// </summary>
        /// <example>Toronto, ON</example>
        public string Origin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the destination location of the load.
        /// </summary>
        /// <example>Vancouver, BC</example>
        public string Destination { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total miles driven for this load.
        /// </summary>
        /// <example>2750.5</example>
        public decimal Miles { get; set; }

        /// <summary>
        /// Gets or sets the pay rate per mile.
        /// </summary>
        /// <example>0.65</example>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the total pay for this load (calculated as Miles × Rate).
        /// </summary>
        /// <example>1787.83</example>
        public decimal Total { get; set; }
    }

}
