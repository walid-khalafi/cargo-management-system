using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        public static string FormatTaxProfile(TaxProfile taxProfile)
        {
            return taxProfile.ToString();

        }

        /// <summary>
        /// Parses a comma-separated address string into an Address value object.
        /// </summary>
        /// <param name="addressString">The comma-separated address string</param>
        /// <returns>An Address value object parsed from the string</returns>
        public static Address ParseAddress(string addressString)
        {
            if (string.IsNullOrWhiteSpace(addressString))
                return new Address("", "", "", "", "");

            var parts = addressString.Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToArray();

            string country = string.Empty;
            string state = string.Empty;
            string zipCode = string.Empty;  
            string city = string.Empty;
            string street = string.Empty;

            if (parts.Length >= 1) street = parts[0];
            if (parts.Length >= 2) city = parts[1];
            if (parts.Length >= 3) state = parts[2];
            if (parts.Length >= 4) zipCode = parts[3];
            if (parts.Length >= 5) country = parts[4];


            return new Address(country,state,city,street,zipCode);
        }


        /// <summary>
        /// Parses a formatted tax profile string into a <see cref="TaxProfile"/> instance.
        /// Expected format: "GstRate:QstRate:PstRate:HstRate:CompoundQstOverGst".
        /// Throws <see cref="FormatException"/> if the input is invalid.
        /// </summary>
        /// <param name="formatted">The formatted tax profile string.</param>
        /// <returns>A new <see cref="TaxProfile"/> instance.</returns>
        public static TaxProfile ParseTaxProfile(string formatted)
        {
            if (string.IsNullOrWhiteSpace(formatted))
                throw new FormatException("TaxProfile string is null or empty.");

            var parts = formatted
                .Split(':', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 5)
                throw new FormatException("TaxProfile must have 5 parts: GstRate:QstRate:PstRate:HstRate:CompoundQstOverGst.");

            try
            {
                var gst = decimal.Parse(parts[0]);
                var qst = decimal.Parse(parts[1]);
                var pst = decimal.Parse(parts[2]);
                var hst = decimal.Parse(parts[3]);
                var compound = bool.Parse(parts[4]);

                return new TaxProfile(gst, qst, pst, hst, compound);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
            {
                throw new FormatException("Failed to parse TaxProfile from string.", ex);
            }
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
