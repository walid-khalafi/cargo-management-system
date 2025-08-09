using Cargo.Domain.Enums;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a geographic location within the cargo management system,
    /// including coordinates, address, and additional metadata.
    /// </summary>
    public record Location
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> record.
        /// Coordinates are validated on creation.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when latitude or longitude are out of range.
        /// </exception>
        public Location(
            double latitude,
            double longitude,
            Address address,
            LocationType locationType,
            double accuracy = 0,
            double? altitude = null,
            string? timezone = null,
            string? facilityName = null,
            string? contactPhone = null,
            string? contactEmail = null,
            OperatingHours? operatingHours = null)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90.");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180.");

            Latitude = latitude;
            Longitude = longitude;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            LocationType = locationType;
            Accuracy = accuracy;
            Altitude = altitude;
            Timezone = timezone;
            FacilityName = facilityName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            OperatingHours = operatingHours;
        }

        /// <summary>Latitude in decimal degrees (-90 to 90).</summary>
        public double Latitude { get; init; }

        /// <summary>Longitude in decimal degrees (-180 to 180).</summary>
        public double Longitude { get; init; }

        /// <summary>Physical address details for this location.</summary>
        public Address Address { get; init; }

        /// <summary>Location accuracy in meters (GPS precision).</summary>
        public double Accuracy { get; init; }

        /// <summary>Altitude in meters above sea level (optional).</summary>
        public double? Altitude { get; init; }

        /// <summary>Timezone identifier (e.g., "Europe/Berlin").</summary>
        public string? Timezone { get; init; }

        /// <summary>Type of location in the logistics domain.</summary>
        public LocationType LocationType { get; init; }

        /// <summary>Facility or building name.</summary>
        public string? FacilityName { get; init; }

        /// <summary>Contact phone number for this location.</summary>
        public string? ContactPhone { get; init; }

        /// <summary>Contact email address for this location.</summary>
        public string? ContactEmail { get; init; }

        /// <summary>Operating hours for the location (structured).</summary>
        public OperatingHours? OperatingHours { get; init; }

        /// <summary>Formatted latitude/longitude coordinates.</summary>
        public string Coordinates => $"{Latitude:F6}, {Longitude:F6}";

        /// <summary>Google Maps link for this location.</summary>
        public string GoogleMapsUrl => $"https://www.google.com/maps?q={Latitude},{Longitude}";

        /// <summary>
        /// Validates that the location has a valid address and coordinates.
        /// </summary>
        public bool IsValid()
        {
            return Address.IsValid();
        }

        /// <summary>
        /// Returns a formatted string combining address and coordinates.
        /// </summary>
        public override string ToString()
        {
            return $"{Address.FullAddress} ({Coordinates})";
        }
    }

    /// <summary>
    /// Represents opening and closing times for a facility.
    /// </summary>
    public record OperatingHours(string OpenTime, string CloseTime);
}