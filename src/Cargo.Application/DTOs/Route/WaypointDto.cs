using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Route
{
    /// <summary>
    /// Represents a waypoint along a route.
    /// </summary>
    /// <remarks>
    /// Waypoints mark specific locations along the route for operational tracking.
    /// </remarks>
    public class WaypointDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the name or description of the waypoint location.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of waypoint (e.g., Pickup, Drop-off, Rest Stop).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sequence order of the waypoint in the route.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate.
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the estimated arrival time at this waypoint (UTC).
        /// </summary>
        public DateTime? EstimatedArrival { get; set; }

        /// <summary>
        /// Gets or sets the actual arrival time at this waypoint (UTC).
        /// </summary>
        public DateTime? ActualArrival { get; set; }
    }

}
