using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Routes
{
    /// <summary>
    /// Data Transfer Object for exposing route information in read operations.
    /// Inherits common audit fields from BaseEntityDto.
    /// </summary>
    public class RouteDto : BaseEntityDto
    {
        /// <summary>Route display name or identifier.</summary>
        public string Name { get; set; }

        /// <summary>Optional route description.</summary>
        public string Description { get; set; }

        /// <summary>Starting point of the route.</summary>
        public string Origin { get; set; }

        /// <summary>Destination of the route.</summary>
        public string Destination { get; set; }

        /// <summary>Ordered list of waypoints between origin and destination.</summary>
        public List<string> Waypoints { get; set; } = new();

        /// <summary>Total route distance in kilometers.</summary>
        public double TotalDistanceKm { get; set; }

        /// <summary>Estimated route duration in minutes.</summary>
        public int EstimatedDurationMinutes { get; set; }

        /// <summary>Estimated fuel cost in system currency.</summary>
        public decimal EstimatedFuelCost { get; set; }

        /// <summary>Estimated toll cost in system currency.</summary>
        public decimal EstimatedTollCost { get; set; }

        /// <summary>Computed total estimated cost (fuel + toll).</summary>
        public decimal TotalEstimatedCost => EstimatedFuelCost + EstimatedTollCost;

        /// <summary>Route type categorization.</summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RouteType RouteType { get; set; }

        /// <summary>Route status.</summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RouteStatus Status { get; set; }


        /// <summary>Assigned vehicle IDs.</summary>
        public List<Guid> AssignedVehicleIds { get; set; } = new();

        /// <summary>Assigned driver IDs.</summary>
        public List<Guid> AssignedDriverIds { get; set; } = new();

        /// <summary>Indicates if the route has the minimum valid data set.</summary>
        public bool IsValid { get; set; }

        /// <summary>Average speed in km/h (TotalDistance / DurationHours).</summary>
        public double AverageSpeedKph { get; set; }

    }
}
