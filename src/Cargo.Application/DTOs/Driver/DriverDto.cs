using System;
using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Driver
{
    /// <summary>
    /// Represents detailed driver information as a Data Transfer Object (DTO) in the cargo management system.
    /// Inherits from <see cref="BaseDto"/> to include unique identifier and audit metadata.
    /// </summary>
    /// <remarks>
    /// Used primarily for GET operations returning full driver profiles, including licensing and company association details.
    /// </remarks>
    public class DriverDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the driver's first name.
        /// </summary>
        /// <example>John</example>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the driver's last name.
        /// </summary>
        /// <example>Doe</example>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the driver's full name (combination of first and last name).
        /// </summary>
        /// <example>John Doe</example>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets or sets the driver's email address.
        /// </summary>
        /// <example>john.doe@example.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the driver's phone number.
        /// </summary>
        /// <example>+1-555-123-4567</example>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the driver's residential address.
        /// </summary>
        public AddressDto Address { get; set; } = new AddressDto();

        /// <summary>
        /// Gets or sets the driver's license number.
        /// </summary>
        /// <example>D1234567</example>
        public string LicenseNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type/category of the driver's license (e.g., Class A, B, C).
        /// </summary>
        /// <example>Class A</example>
        public string LicenseType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiration date of the driver's license (in UTC).
        /// </summary>
        /// <example>2028-05-15T00:00:00Z</example>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the driver's date of birth (in UTC).
        /// </summary>
        /// <example>1985-07-20T00:00:00Z</example>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the total years of professional driving experience.
        /// </summary>
        /// <example>12</example>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Gets or sets the current employment status of the driver.
        /// </summary>
        /// <example>Active</example>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the company the driver is associated with.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid CompanyId { get; set; }
    }
}
