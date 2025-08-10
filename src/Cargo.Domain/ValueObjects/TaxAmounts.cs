using System;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents calculated tax amounts including GST, QST, PST, and HST.
    /// This value object encapsulates the results of tax calculations for a given taxable amount.
    /// </summary>
    public sealed class TaxAmounts
    {
        // For EF Core - parameterless constructor
        public TaxAmounts() { }

        /// <summary>
        /// Gets or sets the calculated Goods and Services Tax (GST) amount.
        /// </summary>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the calculated Quebec Sales Tax (QST) amount.
        /// </summary>
        public decimal QstAmount { get; set; }

        /// <summary>
        /// Gets or sets the calculated Provincial Sales Tax (PST) amount.
        /// </summary>
        public decimal PstAmount { get; set; }

        /// <summary>
        /// Gets or sets the calculated Harmonized Sales Tax (HST) amount.
        /// </summary>
        public decimal HstAmount { get; set; }

        /// <summary>
        /// Gets the total calculated taxes (sum of all tax amounts).
        /// </summary>
        public decimal TotalTaxes => GstAmount + QstAmount + PstAmount + HstAmount;

        /// <summary>
        /// Initializes a new instance of the TaxAmounts class with calculated tax amounts.
        /// </summary>
        /// <param name="gstAmount">The calculated GST amount.</param>
        /// <param name="qstAmount">The calculated QST amount.</param>
        /// <param name="pstAmount">The calculated PST amount.</param>
        /// <param name="hstAmount">The calculated HST amount.</param>
        private TaxAmounts(decimal gstAmount, decimal qstAmount, decimal pstAmount, decimal hstAmount)
        {
            GstAmount = gstAmount;
            QstAmount = qstAmount;
            PstAmount = pstAmount;
            HstAmount = hstAmount;
        }

        /// <summary>
        /// Calculates tax amounts based on the taxable base and tax profile.
        /// </summary>
        /// <param name="taxableBase">The base amount to calculate taxes on.</param>
        /// <param name="profile">The tax profile containing rates and rules.</param>
        /// <param name="decimals">The number of decimal places to round to (default: 2).</param>
        /// <returns>A new instance of <see cref="TaxAmounts"/> with calculated tax amounts.</returns>
        /// <exception cref="ArgumentNullException">Thrown when profile is null.</exception>
        /// <exception cref="ArgumentException">Thrown when taxableBase is negative.</exception>
        public static TaxAmounts Calculate(decimal taxableBase, TaxProfile profile, int decimals = 2, MidpointRounding awayFromZero = default)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));
            
            if (taxableBase < 0)
                throw new ArgumentException("Taxable base cannot be negative", nameof(taxableBase));

            // Calculate GST amount
            decimal gstAmount = Math.Round(taxableBase * profile.GstRate, decimals, MidpointRounding.AwayFromZero);
            
            // Calculate QST amount with compounding if applicable
            decimal qstAmount = 0;
            if (profile.CompoundQstOverGst)
            {
                // Quebec compounding: QST is calculated on amount including GST
                qstAmount = Math.Round((taxableBase + gstAmount) * profile.QstRate, decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                qstAmount = Math.Round(taxableBase * profile.QstRate, decimals, MidpointRounding.AwayFromZero);
            }
            
            // Calculate PST amount
            decimal pstAmount = Math.Round(taxableBase * profile.PstRate, decimals, MidpointRounding.AwayFromZero);
            
            // Calculate HST amount
            decimal hstAmount = Math.Round(taxableBase * profile.HstRate, decimals, MidpointRounding.AwayFromZero);

            return new TaxAmounts(gstAmount, qstAmount, pstAmount, hstAmount);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is TaxAmounts other)
            {
                return GstAmount == other.GstAmount &&
                       QstAmount == other.QstAmount &&
                       PstAmount == other.PstAmount &&
                       HstAmount == other.HstAmount;
            }
            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + GstAmount.GetHashCode();
                hash = hash * 23 + QstAmount.GetHashCode();
                hash = hash * 23 + PstAmount.GetHashCode();
                hash = hash * 23 + HstAmount.GetHashCode();
                return hash;
            }
        }
    }
}
