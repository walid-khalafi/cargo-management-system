namespace Cargo.Application.DTOs.Common
{
    /// <summary>
    /// Represents a vehicle's license plate details as a Data Transfer Object (DTO)
    /// within the cargo management system.
    /// </summary>
    /// <remarks>
    /// Mirrors the <c>PlateNumber</c> value object from the domain layer to enable
    /// safe and consistent data exchange between API layers.
    /// Useful for identifying vehicles in transport, tracking, and compliance workflows.
    /// </remarks>
    public class PlateNumberDto
    {
        /// <summary>
        /// Gets or sets the license plate number, excluding province or country codes.
        /// </summary>
        /// <example>ABC-1234</example>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the province, state, or region associated with the license plate.
        /// </summary>
        /// <example>Ontario</example>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country where the plate is registered.
        /// </summary>
        /// <example>Canada</example>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Returns a string representation combining the plate number and province.
        /// </summary>
        /// <returns>
        /// A string in the format: "Number - Province".
        /// Example: <c>ABC-1234 - Ontario</c>
        /// </returns>
        public override string ToString()
        {
            return $"{Number} - {Province}";
        }
    }
}
