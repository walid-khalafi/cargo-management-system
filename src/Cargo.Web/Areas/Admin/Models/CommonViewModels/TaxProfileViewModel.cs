using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.CommonViewModels
{
    /// <summary>
    /// ViewModel for transferring tax profile data between the UI and the application layer.
    /// Supports model binding and later conversion to the immutable <see cref="Cargo.Domain.ValueObjects.TaxProfile"/>.
    /// </summary>
    public class TaxProfileViewModel
    {
        /// <summary>
        /// Goods and Services Tax (GST) rate as a decimal (e.g., 0.05 for 5%).
        /// Must be between 0 and 1.
        /// </summary>
        [Range(0, 1, ErrorMessage = "GST rate must be between 0 and 1.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal GstRate { get; set; }

        /// <summary>
        /// Quebec Sales Tax (QST) rate as a decimal (e.g., 0.09975 for 9.975%).
        /// Must be between 0 and 1.
        /// </summary>
        [Range(0, 1, ErrorMessage = "QST rate must be between 0 and 1.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal QstRate { get; set; }

        /// <summary>
        /// Provincial Sales Tax (PST) rate as a decimal.
        /// Must be between 0 and 1.
        /// </summary>
        [Range(0, 1, ErrorMessage = "PST rate must be between 0 and 1.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal PstRate { get; set; }

        /// <summary>
        /// Harmonized Sales Tax (HST) rate as a decimal.
        /// Must be between 0 and 1.
        /// </summary>
        [Range(0, 1, ErrorMessage = "HST rate must be between 0 and 1.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal HstRate { get; set; }

        /// <summary>
        /// Indicates whether QST should be calculated on the amount including GST (Quebec compounding).
        /// </summary>
        public bool CompoundQstOverGst { get; set; }
    }
}
