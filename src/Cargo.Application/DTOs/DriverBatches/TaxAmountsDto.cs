namespace Cargo.Application.DTOs.DriverBatches
{
    /// <summary>
    /// DTO for transferring calculated tax amounts (GST, QST, PST, HST).
    /// </summary>
    public class TaxAmountsDto
    {
        public decimal GstAmount { get; set; }
        public decimal QstAmount { get; set; }
        public decimal PstAmount { get; set; }
        public decimal HstAmount { get; set; }

        // Optional: expose total to avoid recomputing on the consumer side
        public decimal TotalTaxes { get; set; }
    }
}


