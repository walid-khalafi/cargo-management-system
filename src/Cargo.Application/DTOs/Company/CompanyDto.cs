using System;
using System.Collections.Generic;
using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Company
{
    /// <summary>
    /// Represents detailed company information as a Data Transfer Object (DTO).
    /// Inherits from <see cref="BaseDto"/> to include unique identifier and audit metadata.
    /// </summary>
    /// <remarks>
    /// This DTO is typically returned in detailed company views or entity retrieval endpoints.
    /// It aggregates address and tax profile information to provide a complete business profile.
    /// </remarks>
    public class CompanyDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the registered legal name of the company.
        /// </summary>
        /// <example>TransCargo Logistics Ltd.</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the official registration or business number.
        /// </summary>
        /// <example>REG-987654321</example>
        public string RegistrationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the registered address of the company.
        /// </summary>
        public AddressDto Address { get; set; } = new AddressDto();

        /// <summary>
        /// Gets or sets the tax profile applicable to this company.
        /// </summary>
        public TaxProfileDto TaxProfile { get; set; } = new TaxProfileDto();

        /// <summary>
        /// Gets or sets the total number of active drivers associated with the company.
        /// </summary>
        /// <example>15</example>
        public int DriverCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of vehicles owned or operated by the company.
        /// </summary>
        /// <example>8</example>
        public int VehicleCount { get; set; }
    }
}
