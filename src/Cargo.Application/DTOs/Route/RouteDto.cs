using System;
using System.Collections.Generic;
using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Route
{
    /// <summary>
    /// Represents detailed information for a transport route within the cargo management system.
    /// </summary>
    /// <remarks>
    /// Inherits from <see cref="BaseDto"/> to include unique identifier and audit metadata.
    /// Includes route details, scheduling, assigned driver and vehicle, and intermediate waypoints.
    /// </remarks>
    public class RouteDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier/code for the route.
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
        /// Gets or sets the current operational status of the route (e.g., Planned, In Progress, Completed).
        /// </summary>
        /// <example>Planned</example>
        public string Status { get; set; } = string.Empty;

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
        /// Gets or sets the actual departure date and time (UTC).
        /// </summary>
        /// <example>2025-08-15T08:15:00Z</example>
        public DateTime? ActualDeparture { get; set; }

        /// <summary>
        /// Gets or sets the actual arrival date and time (UTC).
        /// </summary>
        /// <example>2025-08-16T19:45:00Z</example>
        public DateTime? ActualArrival { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the assigned driver, if applicable.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid? DriverId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the assigned vehicle, if applicable.
        /// </summary>
        /// <example>5dfe12a6-8129-44b9-a2a1-92b9cebc246b</example>
        public Guid? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the list of waypoints along the route.
        /// </summary>
        public List<WaypointDto> Waypoints { get; set; } = new List<WaypointDto>();
    }

}
