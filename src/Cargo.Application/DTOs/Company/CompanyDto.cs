using Cargo.Application.DTOs.Common;
using System;
using System.Collections.Generic;

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
}