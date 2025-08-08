// ==================================================================================
// ENTITY: Route
// ==================================================================================
// Purpose: Represents a route entity in the cargo management system
// This entity encapsulates all route-related information including origin, destination,
// waypoints, distance, time estimates, and operational details
// ==================================================================================

using System;
using System.Collections.Generic;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a route in the cargo management system
    /// This entity contains all information related to a route including origin, destination,
    /// waypoints, distance, time estimates, and operational details
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Initializes a new instance of the Route class with default values
        /// </summary>
        public Route()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Status = "Active";
            Waypoints = new List<string>();
            EstimatedFuelCost = 0;
            EstimatedTollCost = 0;
            TotalDistance = 0;
            EstimatedDuration = TimeSpan.Zero;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the route
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the route name or identifier
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the route description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the starting location of the route
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the destination location of the route
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the collection of waypoints along the route
        /// </summary>
        public virtual ICollection<string> Waypoints { get; set; }

        /// <summary>
        /// Gets or sets the total distance of the route in kilometers
        /// </summary>
        public double TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the estimated duration for the route
        /// </summary>
        public TimeSpan EstimatedDuration { get; set; }

        /// <summary>
        /// Gets or sets the estimated fuel cost for the route
        /// </summary>
        public decimal EstimatedFuelCost { get; set; }

        /// <summary>
        /// Gets or sets the estimated toll cost for the route
        /// </summary>
        public decimal EstimatedTollCost { get; set; }

        /// <summary>
        /// Gets or sets the route type (Highway, Local, Mixed, etc.)
        /// </summary>
        public string RouteType { get; set; }

        /// <summary>
        /// Gets or sets the route status (Active, Inactive, Archived)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date when the route was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date when the route was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the collection of vehicles assigned to this route
        /// </summary>
        public virtual ICollection<Vehicle> AssignedVehicles { get; set; }

        /// <summary>
        /// Gets or sets the collection of drivers assigned to this route
        /// </summary>
        public virtual ICollection<Driver> AssignedDrivers { get; set; }

        /// <summary>
        /// Calculates the total estimated cost for the route
        /// </summary>
        /// <returns>The total estimated cost including fuel and tolls</returns>
        public decimal GetTotalEstimatedCost()
        {
            return EstimatedFuelCost + EstimatedTollCost;
        }

        /// <summary>
        /// Determines if the route is currently active
        /// </summary>
        /// <returns>True if the route is active and within effective date range</returns>
        public bool IsActive()
        {
            return Status == "Active";
        }

        /// <summary>
        /// Calculates the average speed for the route
        /// </summary>
        /// <returns>The average speed in km/h</returns>
        public double GetAverageSpeed()
        {
            if (TotalDistance <= 0 || EstimatedDuration.TotalHours <= 0)
                return 0;

            return TotalDistance / EstimatedDuration.TotalHours;
        }

        /// <summary>
        /// Determines if the route is complete and ready for use
        /// </summary>
        /// <returns>True if the route has valid origin, destination, and distance</returns>
        public bool IsValid()
        {
            return Origin != null && 
                   Destination != null && 
                   TotalDistance > 0 && 
                   EstimatedDuration > TimeSpan.Zero;
        }
    }
}
