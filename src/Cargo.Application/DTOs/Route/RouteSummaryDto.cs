using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Route
{
    /// <summary>
    /// Represents summarized route information for list or overview displays.
    /// </summary>
    /// <remarks>
    /// Used in route listings, dashboards, or quick search results.
    /// Includes high-level route details only.
    /// </remarks>
    public class RouteSummaryDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the route number or code.
        /// </summary>
        public string RouteNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the starting location.
        /// </summary>
        public string Origin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the final destination.
        /// </summary>
        public string Destination { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the distance in kilometers.
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// Gets or sets the current operational status.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scheduled departure date (UTC).
        /// </summary>
        public DateTime? ScheduledDeparture { get; set; }

        /// <summary>
        /// Gets or sets the full name of the assigned driver.
        /// </summary>
        public string DriverName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets vehicle information (e.g., plate number, model).
        /// </summary>
        public string VehicleInfo { get; set; } = string.Empty;
    }

}
