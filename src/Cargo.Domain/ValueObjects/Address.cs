namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a physical address as a value object.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Initializes a new instance of the Address class with default values
        /// </summary>
        public Address()
        {
            Street = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Country = string.Empty;
        }

        /// <summary>
        /// Gets or sets the street address.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets the full address as a formatted string.
        /// </summary>
        public string FullAddress => $"{Street}, {City}, {State} {ZipCode}, {Country}".Trim();
    }
}
