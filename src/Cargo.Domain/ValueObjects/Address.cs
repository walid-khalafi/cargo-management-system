using System;
using System.Linq;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a physical or mailing address in the domain.
    /// Immutable value object with null/whitespace guarding for all fields.
    /// </summary>
    public class Address
    {
        /// <summary>Street name and number.</summary>
        public string Street { get; init; }

        /// <summary>City or town name.</summary>
        public string City { get; init; }

        /// <summary>State or province name.</summary>
        public string State { get; init; }

        /// <summary>Postal or ZIP code.</summary>
        public string ZipCode { get; init; }

        /// <summary>Country name.</summary>
        public string Country { get; init; }

        /// <summary>
        /// Full address as a single formatted string.
        /// Example: "123 Main St, Springfield, IL 62704, USA"
        /// </summary>
        public string FullAddress =>
            string.Join(", ",
                new[] { Street, City, $"{State} {ZipCode}".Trim(), Country }
                .Where(x => !string.IsNullOrWhiteSpace(x))
            );

        /// <summary>
        /// Main constructor ensuring no null or whitespace values are stored.
        /// </summary>
        public Address(string country, string state, string city, string street, string zipCode)
        {
            Country = !string.IsNullOrWhiteSpace(country) ? country : string.Empty;
            State = !string.IsNullOrWhiteSpace(state) ? state : string.Empty;
            City = !string.IsNullOrWhiteSpace(city) ? city : string.Empty;
            Street = !string.IsNullOrWhiteSpace(street) ? street : string.Empty;
            ZipCode = !string.IsNullOrWhiteSpace(zipCode) ? zipCode : string.Empty;
        }

        /// <summary>
        /// Private parameterless constructor for EF Core materialization.
        /// </summary>
        private Address() { }

        /// <summary>
        /// Checks if this address has the minimum required fields.
        /// </summary>
        public bool IsValid() =>
            !string.IsNullOrWhiteSpace(Street) &&
            !string.IsNullOrWhiteSpace(City) &&
            !string.IsNullOrWhiteSpace(State) &&
            !string.IsNullOrWhiteSpace(Country);

        /// <summary>
        /// Returns a human-readable string representation of the address.
        /// </summary>
        public override string ToString() => FullAddress;

        /// <summary>
        /// Parses a formatted string into an Address object.
        /// Expected format: "Street, City, State ZipCode, Country".
        /// Throws FormatException if invalid.
        /// </summary>
        public static Address Parse(string formatted)
        {
            if (string.IsNullOrWhiteSpace(formatted))
                throw new FormatException("Address string is null or empty.");

            var parts = formatted
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToArray();

            if (parts.Length != 4)
                throw new FormatException("Address must have 4 parts: Street, City, State ZipCode, Country.");

            var street = parts[0];
            var city = parts[1];
            var stateZip = parts[2];
            var country = parts[3];

            var tokens = stateZip.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 2)
                throw new FormatException("State and ZipCode segment is invalid.");

            var zipCode = tokens.Last();
            var state = string.Join(" ", tokens.Take(tokens.Length - 1));

            return new Address(country, state, city, street, zipCode);
        }

        /// <summary>
        /// Attempts to parse without throwing exceptions.
        /// </summary>
        public static bool TryParse(string formatted, out Address address)
        {
            try
            {
                address = Parse(formatted);
                return true;
            }
            catch
            {
                address = default!;
                return false;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            obj is Address other &&
            Street == other.Street &&
            City == other.City &&
            State == other.State &&
            ZipCode == other.ZipCode &&
            Country == other.Country;

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(Street, City, State, ZipCode, Country);
    }
}