using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Routes
{
    /// <summary>
    /// Payload for creating a new route.
    /// Duration is provided in minutes to simplify API binding.
    /// </summary>
    public class RouteCreateDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required, StringLength(200)]
        public string Origin { get; set; }

        [Required, StringLength(200)]
        public string Destination { get; set; }

        public List<string> Waypoints { get; set; } = new();

        [Range(0.1, 50000)]
        public double TotalDistanceKm { get; set; }

        [Range(1, 43200)]
        public int EstimatedDurationMinutes { get; set; }
        [Range(0, 100000000)]
        public decimal EstimatedFuelCost { get; set; }

        [Range(0, 100000000)]
        public decimal EstimatedTollCost { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(RouteType))]
        public RouteType RouteType { get; set; } = RouteType.Mixed;

        public List<Guid> AssignedVehicleIds { get; set; } = new();
        public List<Guid> AssignedDriverIds { get; set; } = new();
    }
}
