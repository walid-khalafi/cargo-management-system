using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Driver
{
    /// <summary>
    /// DTO for returning detailed driver information to clients.
    /// Mirrors the Driver entity but exposes only safe, client-facing fields.
    /// </summary>
    public class DriverDto : BaseEntityDto
    {
        /// <summary>
        /// Driver's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Driver's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Full name of the driver (first + last).
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email address for communication.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Primary contact number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Physical address in formatted string form.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Driver's license number.
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Type of driver's license (e.g., CDL-A, CDL-B).
        /// </summary>
        public string LicenseType { get; set; }

        /// <summary>
        /// Date when the license expires.
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Years of driving experience.
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Current operational status of the driver.
        /// </summary>
        public DriverStatus Status { get; set; }

        /// <summary>
        /// Identifier of the company this driver belongs to.
        /// </summary>
        public Guid CompanyId { get; set; }


    }

    /// <summary>
    /// DTO for creating a new driver record.
    /// Used as input when registering a new driver in the system.
    /// </summary>
    public class DriverCreateDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, MaxLength(50)]
        public string LicenseNumber { get; set; }

        [Required, MaxLength(20)]
        public string LicenseType { get; set; }

        [Required]
        public DateTime LicenseExpiryDate { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, Range(0, 80)]
        public int YearsOfExperience { get; set; }

        [Required]
        public DriverStatus Status { get; set; } = DriverStatus.Active;

        [Required]
        public Guid CompanyId { get; set; }


    }
    /// <summary>
    /// DTO for updating an existing driver's details.
    /// All fields are optional; only provided values will be updated.
    /// </summary>
    public class DriverUpdateDto
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        [MaxLength(20)]
        public string LicenseType { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Range(0, 80)]
        public int? YearsOfExperience { get; set; }
        public DriverStatus? Status { get; set; }

        public Guid? CompanyId { get; set; }

    }
}
