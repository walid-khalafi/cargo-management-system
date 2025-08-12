using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Company
{
    /// <summary>
    /// Represents the data required to update an existing company record.
    /// </summary>
    /// <remarks>
    /// Used as input for HTTP PUT/PATCH requests when modifying company details.
    /// </remarks>
    public class UpdateCompanyDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the registered legal name of the company.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the official registration or business number.
        /// </summary>
        public string RegistrationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the registered address of the company.
        /// </summary>
        public AddressDto Address { get; set; } = new AddressDto();

        /// <summary>
        /// Gets or sets the tax profile applicable to this company.
        /// </summary>
        public TaxProfileDto TaxProfile { get; set; } = new TaxProfileDto();
    }
}
