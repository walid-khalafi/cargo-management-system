using System;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a geographic location as a value object in the cargo management system.
    /// This value object encapsulates all location-related information including coordinates,
    /// address details, and additional metadata for precise location tracking.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Initializes a new instance of the Location class with default values
        /// </summary>
        public Location()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Accuracy = 0;
            IsVerified = false;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this location instance
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate in decimal degrees (-90 to 90)
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate in decimal degrees (-180 to 180)
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the street address or detailed location description
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city or town name
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state, province, or region
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postal or ZIP code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country name
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the location accuracy in meters (GPS accuracy)
        /// </summary>
        public double Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the altitude in meters above sea level
        /// </summary>
        public double? Altitude { get; set; }

        /// <summary>
        /// Gets or sets the timezone identifier (e.g., "America/New_York")
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the location type (e.g., "Warehouse", "Depot", "Customer", "Port")
        /// </summary>
        public string LocationType { get; set; }

        /// <summary>
        /// Gets or sets the facility or building name
        /// </summary>
        public string FacilityName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number for this location
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the contact email for this location
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the operating hours for this location
        /// </summary>
        public string OperatingHours { get; set; }

        /// <summary>
        /// Gets or sets the date when this location was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date when this location was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets whether this location has been verified for accuracy
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets the full formatted address as a single string
        /// </summary>
        public string FullAddress => $"{Address}, {City}, {State} {PostalCode}, {Country}".Trim();

        /// <summary>
        /// Gets the location coordinates as a formatted string
        /// </summary>
        public string Coordinates => $"{Latitude:F6}, {Longitude:F6}";

        /// <summary>
        /// Gets the Google Maps URL for this location
        /// </summary>
        public string GoogleMapsUrl => $"https://www.google.com/maps?q={Latitude},{Longitude}";

        /// <summary>
        /// Calculates the distance to another location using the Haversine formula
        /// </summary>
        /// <param name="other">The target location</param>
        /// <returns>Distance in kilometers</returns>
        public double DistanceTo(Location other)
        {
            if (other == null) return 0;

            const double earthRadius = 6371; // Earth's radius in kilometers
            var lat1 = Latitude * Math.PI / 180;
            var lat2 = other.Latitude * Math.PI / 180;
            var deltaLat = (other.Latitude - Latitude) * Math.PI / 180;
            var deltaLon = (other.Longitude - Longitude) * Math.PI / 180;

            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadius * c;
        }

        /// <summary>
        /// Determines if the location is valid (has coordinates and address)
        /// </summary>
        /// <returns>True if the location has valid coordinates and address</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Address) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(State) &&
                   !string.IsNullOrWhiteSpace(Country) &&
                   Latitude >= -90 && Latitude <= 90 &&
                   Longitude >= -180 && Longitude <= 180;
        }

        /// <summary>
        /// Returns a string representation of the location
        /// </summary>
        /// <returns>Formatted string with location details</returns>
        public override string ToString()
        {
            return $"{FullAddress} ({Coordinates})";
        }
    }
}
