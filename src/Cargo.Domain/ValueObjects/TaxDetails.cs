using System;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents tax details including GST and QST amounts.
    /// </summary>
    public class TaxDetails
    {
        // For EF Core - parameterless constructor
        public TaxDetails() { }

        /// <summary>
        /// Gets or sets the GST amount.
        /// </summary>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the QST amount.
        /// </summary>
        public decimal QstAmount { get; set; }

        /// <summary>
        /// Gets the total calculated taxes (GST + QST).
        /// </summary>
        public decimal TotalTaxes => GstAmount + QstAmount;

        /// <summary>
        /// Calculates the tax details based on the taxable base and tax rates.
        /// </summary>
        /// <param name="taxableBase">The taxable base amount.</param>
        /// <param name="gstRate">The GST rate (e.g., 0.05m for 5%).</param>
        /// <param name="qstRate">The QST rate (e.g., 0.09975m for 9.975%).</param>
        /// <param name="decimals">Number of decimal places to round to.</param>
        /// <returns>A new instance of <see cref="TaxDetails"/> with calculated amounts.</returns>
        public static TaxDetails Calculate(decimal taxableBase, decimal gstRate, decimal qstRate, int decimals = 2)
        {
            var gstAmount = Math.Round(taxableBase * gstRate, decimals, MidpointRounding.AwayFromZero);
            var qstAmount = Math.Round(taxableBase * qstRate, decimals, MidpointRounding.AwayFromZero);
            
            var result = new TaxDetails();
            result.GstAmount = gstAmount;
            result.QstAmount = qstAmount;
            return result;
        }
    }
}
