namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a physical address as a value object,
    /// encapsulating street, city, state, postal code, and country information.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class
        /// with default empty values.
        /// </summary>
        public Address()
        {
            Street = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Country = string.Empty;
        }

        /// <summary>Street name and number.</summary>
        public string Street { get; set; }

        /// <summary>City or town name.</summary>
        public string City { get; set; }

        /// <summary>State or province name.</summary>
        public string State { get; set; }

        /// <summary>Postal or ZIP code.</summary>
        public string ZipCode { get; set; }

        /// <summary>Country name.</summary>
        public string Country { get; set; }

        /// <summary>
        /// Returns the full address as a single formatted string.
        /// </summary>
        public string FullAddress =>
            $"{Street}, {City}, {State} {ZipCode}, {Country}".Trim();

        /// <summary>
        /// Checks whether this address has the minimum required fields.
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(State) &&
                   !string.IsNullOrWhiteSpace(Country);
        }

        /// <summary>
        /// Returns a readable string representation of the address.
        /// </summary>
        public override string ToString() => FullAddress;
    }
}