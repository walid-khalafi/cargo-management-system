using System.ComponentModel.DataAnnotations;

namespace Cargo.Application.DTOs.Company
{
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
}