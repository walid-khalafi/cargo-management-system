namespace Cargo.Application.DTOs.Common
{
    /// <summary>
    /// Represents an address data transfer object (DTO) used across the cargo management system.
    /// Encapsulates standardized address information to ensure consistency in API requests and responses.
    /// </summary>
    /// <remarks>
    /// This DTO mirrors the structure of the <c>Address</c> value object in the domain layer.
    /// It is designed for safe data exchange between application layers, ensuring immutability in domain models.
    /// </remarks>
    public class AddressDto
    {
        /// <summary>
        /// Gets or sets the street name and number of the address.
        /// </summary>
        /// <example>1234 Elm Street</example>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city or locality name.
        /// </summary>
        /// <example>Berlin</example>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the province, state, or region of the address.
        /// </summary>
        /// <example>Brandenburg</example>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal or ZIP code associated with the address.
        /// </summary>
        /// <example>10115</example>
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        /// <example>Germany</example>
        public string Country { get; set; } = string.Empty;
    }
}
