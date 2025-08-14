using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Cargo.Application.Mapping.Helpers
{
    /// <summary>
    /// Helper class for mapping operations used in mapping profiles.
    /// Provides common functionality for converting between value objects and string representations.
    /// </summary>
    public static class MappingHelper
    {
        /// <summary>
        /// Formats an Address value object into a single comma-separated string.
        /// </summary>
        /// <param name="address">The address value object to format</param>
        /// <returns>A formatted string representation of the address</returns>
        public static string FormatAddress(Address address)
        {
            if (address == null) return string.Empty;

            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(address.Street)) parts.Add(address.Street);
            if (!string.IsNullOrWhiteSpace(address.City)) parts.Add(address.City);
            if (!string.IsNullOrWhiteSpace(address.State)) parts.Add(address.State);
            if (!string.IsNullOrWhiteSpace(address.ZipCode)) parts.Add(address.ZipCode);
            if (!string.IsNullOrWhiteSpace(address.Country)) parts.Add(address.Country);

            return string.Join(", ", parts);
        }

        /// <summary>
        /// Parses a comma-separated address string into an Address value object.
        /// </summary>
        /// <param name="addressString">The comma-separated address string</param>
        /// <returns>An Address value object parsed from the string</returns>
        public static Address ParseAddress(string addressString)
        {
            if (string.IsNullOrWhiteSpace(addressString))
                return new Address();

            var parts = addressString.Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToArray();

            var address = new Address();
            
            if (parts.Length >= 1) address.Street = parts[0];
            if (parts.Length >= 2) address.City = parts[1];
            if (parts.Length >= 3) address.State = parts[2];
            if (parts.Length >= 4) address.ZipCode = parts[3];
            if (parts.Length >= 5) address.Country = parts[4];

            return address;
        }

        /// <summary>
        /// Creates a TaxProfile value object from a string representation.
        /// </summary>
        /// <param name="taxProfileString">The string representation of the tax profile</param>
        /// <returns>A TaxProfile value object</returns>
        public static TaxProfile CreateTaxProfile(string taxProfileString)
        {
            if (string.IsNullOrWhiteSpace(taxProfileString))
                return TaxProfile.CreateQuebecProfile();

            var value = taxProfileString.Trim();

            if (value.Equals("Quebec", StringComparison.OrdinalIgnoreCase))
                return TaxProfile.CreateQuebecProfile();

            if (value.Equals("Ontario", StringComparison.OrdinalIgnoreCase))
                return TaxProfile.CreateOntarioProfile();

            // Default/fallback
            return TaxProfile.CreateQuebecProfile();
        }


        // Helpers to build domain objects used in mapping tests
        public static Vehicle BuildVehicle(Guid id, string make, string model, string plateValue)
        {
            
             return  new Vehicle(
                make,
                model,
                2023,
                "Blue",
                "1FTFW1E14PFA12345",
                "REG123",
                new PlateNumber(plateValue, "QA", "Standard"),
                "Diesel",
                5,
                Guid.NewGuid()
            );

         
        }

        public static Company BuildCompany(Guid id, string name)
        {
            return new Company
            {
                Id = id,
                Name = name
            };
        }




    }
}
