using System.ComponentModel.DataAnnotations;

namespace Cargo.Web.Areas.Admin.Models.CompanyViewModels
{
    public class TaxProfileViewModel
    {
        [Required(ErrorMessage = "GST Rate is required")]
        [Range(0, 1, ErrorMessage = "GST Rate must be between 0 and 1")]
        [Display(Name = "GST Rate (%)")]
        public decimal GstRate { get; set; }

        [Required(ErrorMessage = "QST Rate is required")]
        [Range(0, 1, ErrorMessage = "QST Rate must be between 0 and 1")]
        [Display(Name = "QST Rate (%)")]
        public decimal QstRate { get; set; }

        [Required(ErrorMessage = "PST Rate is required")]
        [Range(0, 1, ErrorMessage = "PST Rate must be between 0 and 1")]
        [Display(Name = "PST Rate (%)")]
        public decimal PstRate { get; set; }

        [Required(ErrorMessage = "HST Rate is required")]
        [Range(0, 1, ErrorMessage = "HST Rate must be between 0 and 1")]
        [Display(Name = "HST Rate (%)")]
        public decimal HstRate { get; set; }

        [Display(Name = "Compound QST over GST")]
        public bool CompoundQstOverGst { get; set; }

        public decimal TotalTaxRate => GstRate + QstRate + PstRate + HstRate;
    }
}
