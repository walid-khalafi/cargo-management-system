// ==================================================================================
// ENTITY: Driver
// ==================================================================================
// Purpose: Represents a driver entity in the cargo management system
// This entity encapsulates all driver-related information including personal details,
// licensing information, and operational status
// ==================================================================================

using System;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a driver in the cargo management system
    /// This entity contains all information related to a driver including personal details,
    /// licensing information, and operational status
    /// </summary>
    public class Driver : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the Driver class with default values
        /// Sets up the driver with a new GUID, current timestamp, and active status
        /// </summary>
        public Driver()
        {
            Status = DriverStatus.Active;
            Address = new Address();
            Settings = new DriverSettings(1, 0, 0, FscMode.Fixed, 0, 0, "", TaxProfile.CreateOntarioProfile());
        }

        /// <summary>
        /// Gets or sets the driver's first name
        /// Required field for driver identification
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the driver's last name
        /// Required field for driver identification
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the driver's full name by combining first and last name
        /// Computed property for convenience
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets or sets the driver's email address
        /// Used for communication and notifications
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the driver's phone number
        /// Primary contact number for the driver
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the driver's physical address
        /// Value object containing complete address information
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the driver's license number
        /// Unique identifier from the licensing authority
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of license held by the driver
        /// Examples: CDL-A, CDL-B, CDL-C, etc.
        /// </summary>
        public string LicenseType { get; set; }

        /// <summary>
        /// Gets or sets the date when the driver's license expires
        /// Used to track license validity and renewal requirements
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// Determines if the driver's license is currently valid
        /// Checks if the license expiry date is in the future
        /// </summary>
        /// <returns>True if license is valid, false otherwise</returns>
        public bool IsLicenseValid()
        {
            return LicenseExpiryDate > DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the driver's date of birth
        /// Used for age verification and compliance checks
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Calculates the driver's current age based on date of birth
        /// </summary>
        /// <returns>The driver's age in years</returns>
        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Gets or sets the number of years of driving experience
        /// Used for driver qualification and assignment decisions
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Gets or sets the current operational status of the driver
        /// </summary>
        public DriverStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the company identifier the driver belongs to.
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company the driver belongs to.
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Gets or sets the driver's specific settings for pay and tax calculations.
        /// </summary>
        public DriverSettings Settings { get; set; }

        /// <summary>
        /// Validates if the driver meets minimum requirements for active duty
        /// Checks license validity, age, and experience requirements
        /// </summary>
        /// <returns>True if driver meets all requirements, false otherwise</returns>
        public bool IsEligibleForActiveDuty()
        {
            return IsLicenseValid() && 
                   GetAge() >= 21 && 
                   YearsOfExperience >= 1 && 
                   Status == DriverStatus.Active;
        }
    }
}
