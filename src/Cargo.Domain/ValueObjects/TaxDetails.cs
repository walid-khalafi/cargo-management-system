using System;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents tax details including GST and QST amounts.
    /// </summary>
    public class TaxDetails
    {
        /// <summary>
        /// Gets the GST amount.
        /// </summary>
        public decimal GstAmount { get; private set; }

        /// <summary>
        /// Gets the QST amount.
        /// </summary>
        public decimal QstAmount { get; private set; }

        /// <summary>
        /// Gets the total taxes (GST + QST).
        /// </summary>
        public decimal TotalTaxes => GstAmount + QstAmount;

        private TaxDetails(decimal gstAmount, decimal qstAmount)
        {
            GstAmount = gstAmount;
            QstAmount = qstAmount;
        }

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
            return new TaxDetails(gstAmount, qstAmount);
        }
    }
}
