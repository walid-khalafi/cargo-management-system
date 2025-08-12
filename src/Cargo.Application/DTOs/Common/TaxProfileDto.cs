namespace Cargo.Application.DTOs.Common
{
    /// <summary>
    /// Represents tax configuration details for a specific province or region
    /// within the cargo management system.
    /// </summary>
    /// <remarks>
    /// Mirrors the <c>TaxProfile</c> value object from the domain layer.
    /// Used for applying the correct tax rates in invoicing, payroll, and compliance workflows.
    /// Includes federal, provincial, and region-specific tax rates (e.g., GST, QST, HST).
    /// </remarks>
    public class TaxProfileDto
    {
        /// <summary>
        /// Gets or sets the province or region associated with this tax profile.
        /// </summary>
        /// <example>Quebec</example>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Goods and Services Tax rate (as a decimal fraction).
        /// </summary>
        /// <example>0.05</example>
        public decimal GstRate { get; set; }

        /// <summary>
        /// Gets or sets the Quebec Sales Tax rate (as a decimal fraction).
        /// </summary>
        /// <example>0.09975</example>
        public decimal QstRate { get; set; }

        /// <summary>
        /// Gets or sets the Harmonized Sales Tax rate (as a decimal fraction).
        /// </summary>
        /// <example>0.13</example>
        public decimal HstRate { get; set; }

        /// <summary>
        /// Gets or sets the federal income tax rate applicable to this profile.
        /// </summary>
        /// <example>0.15</example>
        public decimal FederalTaxRate { get; set; }

        /// <summary>
        /// Gets or sets the provincial or territorial income tax rate for this profile.
        /// </summary>
        /// <example>0.10</example>
        public decimal ProvincialTaxRate { get; set; }

        /// <summary>
        /// Indicates whether this tax profile follows Quebec-specific taxation rules.
        /// </summary>
        /// <example>true</example>
        public bool IsQuebecProfile { get; set; }

        /// <summary>
        /// Indicates whether this tax profile follows Ontario-specific taxation rules.
        /// </summary>
        /// <example>false</example>
        public bool IsOntarioProfile { get; set; }
    }
}
