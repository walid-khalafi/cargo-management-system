using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Company
{
    /// <summary>
    /// Data transfer object for creating a new company
    /// </summary>
    public class CreateCompanyDto
    {
        /// <summary>
        /// Gets or sets the name of the company
        /// </summary>
        /// <example>ABC Transport Inc.</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the business registration number
        /// </summary>
        /// <example>123456789</example>
        public string RegistrationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company's physical address
        /// </summary>
        public AddressDto Address { get; set; } = new AddressDto();

        /// <summary>
        /// Gets or sets the tax profile for this company
        /// </summary>
        public TaxProfileDto TaxProfile { get; set; } = new TaxProfileDto();
    }
}
