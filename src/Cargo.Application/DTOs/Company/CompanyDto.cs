using Cargo.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.Company
{
    /// <summary>
    /// DTO for returning detailed company information to clients.
    /// Includes identification, registration, address, tax profile, and related entity IDs.
    /// </summary>
    public class CompanyDto : BaseEntityDto
    {
        /// <summary>
        /// Registered name of the company.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Official company registration number issued by the relevant authority.
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Physical address of the company in a formatted string.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Name or type of the tax profile associated with this company.
        /// </summary>
        public string TaxProfile { get; set; }

        /// <summary>
        /// List of IDs of drivers employed by this company.
        /// Always returned as an empty list if there are no drivers.
        /// </summary>
        public List<Guid> DriverIds { get; set; } = new();

        /// <summary>
        /// List of IDs of vehicles owned or registered under this company.
        /// Always returned as an empty list if there are no vehicles.
        /// </summary>
        public List<Guid> VehicleIds { get; set; } = new();
    }

    /// <summary>
    /// DTO for creating a new company record.
    /// Used as input when registering a company in the system.
    /// </summary>
    public class CompanyCreateDto
    {
        /// <summary>
        /// Registered name of the company.
        /// </summary>
        [Required, MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Official company registration number.
        /// </summary>
        [Required, MaxLength(100)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Company's physical address in structured or string form.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Tax profile type for the company.
        /// </summary>
        [Required]
        public string TaxProfile { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing company's details.
    /// Fields are optional — only provided values will be updated.
    /// </summary>
    public class CompanyUpdateDto
    {
        /// <summary>
        /// New company name (if being updated).
        /// </summary>
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Updated company registration number.
        /// </summary>
        [MaxLength(100)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Updated address for the company.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Updated tax profile for the company.
        /// </summary>
        public string TaxProfile { get; set; }
    }
}