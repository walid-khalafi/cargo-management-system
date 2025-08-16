using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.Company
{
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