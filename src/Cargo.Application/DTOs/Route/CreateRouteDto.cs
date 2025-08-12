namespace Cargo.Application.DTOs.Route
{
    /// <summary>
    /// Represents the required data for creating a new route.
    /// </summary>
    /// <remarks>
    /// Used in HTTP POST requests to register a new transport route.
    /// Contains route identification, locations, distance, schedule, and planned waypoints.
    /// </remarks>
    public class CreateRouteDto
    {
        /// <summary>
        /// Gets or sets the unique identifier or code for the route.
        /// </summary>
        /// <example>ROUTE-2025-001</example>
        public string RouteNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the starting location for the route.
        /// </summary>
        /// <example>Dubai, UAE</example>
        public string Origin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the final destination for the route.
        /// </summary>
        /// <example>Riyadh, Saudi Arabia</example>
        public string Destination { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total route distance in kilometers.
        /// </summary>
        /// <example>895.4</example>
        public decimal Distance { get; set; }

        /// <summary>
        /// Gets or sets the estimated duration to complete the route.
        /// </summary>
        /// <example>12:30:00</example>
        public TimeSpan EstimatedDuration { get; set; }

        /// <summary>
        /// Gets or sets the type of route (e.g., Domestic, International).
        /// </summary>
        /// <example>International</example>
        public string RouteType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scheduled departure date and time (UTC).
        /// </summary>
        /// <example>2025-08-15T08:00:00Z</example>
        public DateTime? ScheduledDeparture { get; set; }

        /// <summary>
        /// Gets or sets the scheduled arrival date and time (UTC).
        /// </summary>
        /// <example>2025-08-16T20:00:00Z</example>
        public DateTime? ScheduledArrival { get; set; }

        /// <summary>
        /// Gets or sets the list of planned waypoints for this route.
        /// </summary>
        public List<CreateWaypointDto> Waypoints { get; set; } = new();
    }

}
