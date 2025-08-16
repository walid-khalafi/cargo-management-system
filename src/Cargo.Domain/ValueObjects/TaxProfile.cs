using System;
using System.Collections.Generic;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a tax profile for calculating Canadian taxes including GST, QST, PST, and HST.
    /// This value object encapsulates all tax rates and rules for a specific province or territory.
    /// </summary>
    /// <remarks>
    /// Supports both simple and compound tax calculations, particularly for Quebec's QST over GST compounding.
    /// </remarks>
    public sealed class TaxProfile
    {
        /// <summary>
        /// Gets the Goods and Services Tax (GST) rate as a decimal (e.g., 0.05 for 5%).
        /// </summary>
        public decimal GstRate { get; }

        /// <summary>
        /// Gets the Quebec Sales Tax (QST) rate as a decimal (e.g., 0.09975 for 9.975%).
        /// </summary>
        public decimal QstRate { get; }

        /// <summary>
        /// Gets the Provincial Sales Tax (PST) rate as a decimal.
        /// </summary>
        public decimal PstRate { get; }

        /// <summary>
        /// Gets the Harmonized Sales Tax (HST) rate as a decimal.
        /// </summary>
        public decimal HstRate { get; }

        /// <summary>
        /// Gets a value indicating whether QST should be calculated on the amount including GST (Quebec compounding).
        /// </summary>
        public bool CompoundQstOverGst { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxProfile"/> class with specified tax rates.
        /// </summary>
        /// <param name="gstRate">The GST rate as a decimal between 0 and 1.</param>
        /// <param name="qstRate">The QST rate as a decimal between 0 and 1.</param>
        /// <param name="pstRate">The PST rate as a decimal between 0 and 1.</param>
        /// <param name="hstRate">The HST rate as a decimal between 0 and 1.</param>
        /// <param name="compoundQstOverGst">True if QST should be compounded over GST (Quebec).</param>
        /// <exception cref="ArgumentException">Thrown when any tax rate is outside the valid range (0-1).</exception>
        public TaxProfile(
            decimal gstRate,
            decimal qstRate,
            decimal pstRate,
            decimal hstRate,
            bool compoundQstOverGst)
        {
            // Validate all tax rates are within valid range (0-1)
            ValidateTaxRate(gstRate, nameof(gstRate));
            ValidateTaxRate(qstRate, nameof(qstRate));
            ValidateTaxRate(pstRate, nameof(pstRate));
            ValidateTaxRate(hstRate, nameof(hstRate));

            GstRate = gstRate;
            QstRate = qstRate;
            PstRate = pstRate;
            HstRate = hstRate;
            CompoundQstOverGst = compoundQstOverGst;
        }

        /// <summary>
        /// Validates that a tax rate is within the valid range (0-1).
        /// </summary>
        /// <param name="rate">The tax rate to validate.</param>
        /// <param name="paramName">The parameter name for error messages.</param>
        /// <exception cref="ArgumentException">Thrown when the rate is outside the valid range.</exception>
        private static void ValidateTaxRate(decimal rate, string paramName)
        {
            if (rate < 0 || rate > 1)
            {
                throw new ArgumentException($"{paramName} must be between 0 and 1", paramName);
            }
        }

        /// <summary>
        /// Gets the total combined tax rate for the profile.
        /// </summary>
        /// <returns>The sum of all applicable tax rates.</returns>
        public decimal GetTotalTaxRate()
        {
            return GstRate + QstRate + PstRate + HstRate;
        }

        /// <summary>
        /// Creates a standard tax profile for Quebec.
        /// </summary>
        /// <returns>A TaxProfile configured for Quebec taxes.</returns>
        public static TaxProfile CreateQuebecProfile()
        {
            return new TaxProfile(
                gstRate: 0.05m,      // 5% GST
                qstRate: 0.09975m,   // 9.975% QST
                pstRate: 0m,         // No PST in Quebec
                hstRate: 0m,         // No HST in Quebec
                compoundQstOverGst: true);
        }

        /// <summary>
        /// Creates a standard tax profile for Ontario.
        /// </summary>
        /// <returns>A TaxProfile configured for Ontario taxes.</returns>
        public static TaxProfile CreateOntarioProfile()
        {
            return new TaxProfile(
                gstRate: 0m,         // No separate GST in Ontario
                qstRate: 0m,         // No QST in Ontario
                pstRate: 0m,         // No PST in Ontario
                hstRate: 0.13m,      // 13% HST
                compoundQstOverGst: false);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is TaxProfile other)
            {
                return GstRate == other.GstRate &&
                       QstRate == other.QstRate &&
                       PstRate == other.PstRate &&
                       HstRate == other.HstRate &&
                       CompoundQstOverGst == other.CompoundQstOverGst;
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
                hash = hash * 23 + GstRate.GetHashCode();
                hash = hash * 23 + QstRate.GetHashCode();
                hash = hash * 23 + PstRate.GetHashCode();
                hash = hash * 23 + HstRate.GetHashCode();
                hash = hash * 23 + CompoundQstOverGst.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(TaxProfile? left, TaxProfile? right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        public static bool operator !=(TaxProfile? left, TaxProfile? right)
        {
            return !(left == right);
        }



        /// <summary>
        /// Returns the combined tax profile as a formatted string.
        /// Format: "GstRate:QstRate:PstRate:HstRate:CompoundQstOverGst".
        /// </summary>
        public override string ToString() =>
            $"{GstRate}:{QstRate}:{PstRate}:{HstRate}:{CompoundQstOverGst}";


        /// <summary>
        /// Parses a formatted tax profile string into a <see cref="TaxProfile"/> instance.
        /// Expected format: "GstRate:QstRate:PstRate:HstRate:CompoundQstOverGst".
        /// Throws <see cref="FormatException"/> if the input is invalid.
        /// </summary>
        /// <param name="formatted">The formatted tax profile string.</param>
        /// <returns>A new <see cref="TaxProfile"/> instance.</returns>
        public static TaxProfile Parse(string formatted)
        {
            if (string.IsNullOrWhiteSpace(formatted))
                throw new FormatException("TaxProfile string is null or empty.");

            var parts = formatted
                .Split(':', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 5)
                throw new FormatException("TaxProfile must have 5 parts: GstRate:QstRate:PstRate:HstRate:CompoundQstOverGst.");

            try
            {
                var gst = decimal.Parse(parts[0]);
                var qst = decimal.Parse(parts[1]);
                var pst = decimal.Parse(parts[2]);
                var hst = decimal.Parse(parts[3]);
                var compound = bool.Parse(parts[4]);

                return new TaxProfile(gst, qst, pst, hst, compound);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
            {
                throw new FormatException("Failed to parse TaxProfile from string.", ex);
            }
        }

        /// <summary>
        /// Attempts to parse the formatted tax profile string into a <see cref="TaxProfile"/>.
        /// Returns <c>true</c> on success; otherwise <c>false</c> (no exception).
        /// </summary>
        /// <param name="formatted">The formatted tax profile string.</param>
        /// <param name="profile">When this method returns, contains the parsed <see cref="TaxProfile"/>, if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string formatted, out TaxProfile profile)
        {
            try
            {
                profile = Parse(formatted);
                return true;
            }
            catch
            {
                profile = null!;
                return false;
            }
        }



    }
}
