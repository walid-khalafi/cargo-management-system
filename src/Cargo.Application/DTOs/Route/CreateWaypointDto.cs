namespace Cargo.Application.DTOs.Route
{
    /// <summary>
    /// Represents the required data for creating a waypoint on a route.
    /// </summary>
    /// <remarks>
    /// Typically used inside <see cref="CreateRouteDto"/> during route creation.
    /// </remarks>
    public class CreateWaypointDto
    {
        /// <summary>
        /// Gets or sets the waypoint location name or description.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the waypoint type (e.g., Pickup, Drop-off, Checkpoint).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the order of the waypoint along the route.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the waypoint.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the waypoint.
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the estimated UTC arrival time at this waypoint.
        /// </summary>
        public DateTime? EstimatedArrival { get; set; }
    }

}
