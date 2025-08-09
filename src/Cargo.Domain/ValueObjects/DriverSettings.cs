using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents driver-specific settings for pay and tax calculations.
    /// This value object encapsulates configuration needed to calculate pay for non-owner drivers.
    /// </summary>
    public sealed class DriverSettings
    {
        /// <summary>
        /// Gets the number of pay bands used for rate calculations.
        /// </summary>
        public int NumPayBands { get; }

        /// <summary>
        /// Gets the hourly rate for time-based pay calculations.
        /// </summary>
        public decimal HourlyRate { get; }

        /// <summary>
        /// Gets the fuel surcharge rate.
        /// </summary>
        public decimal FscRate { get; }

        /// <summary>
        /// Gets the fuel surcharge calculation mode (per mile or percentage).
        /// </summary>
        public FscMode FscMode { get; }

        /// <summary>
        /// Gets the waiting time rate per minute.
        /// </summary>
        public decimal WaitingPerMinute { get; }

        /// <summary>
        /// Gets the administrative fee percentage or flat amount.
        /// </summary>
        public decimal AdminFee { get; }

        /// <summary>
        /// Gets the province for tax calculation purposes.
        /// </summary>
        public string Province { get; }

        /// <summary>
        /// Gets the tax profile for this driver's location.
        /// </summary>
        public TaxProfile TaxProfile { get; }

        /// <summary>
        /// Initializes a new instance of the DriverSettings class.
        /// </summary>
        /// <param name="numPayBands">The number of pay bands.</param>
        /// <param name="hourlyRate">The hourly rate.</param>
        /// <param name="fscRate">The fuel surcharge rate.</param>
        /// <param name="fscMode">The fuel surcharge calculation mode.</param>
        /// <param name="waitingPerMinute">The waiting time rate per minute.</param>
        /// <param name="adminFee">The administrative fee.</param>
        /// <param name="province">The province for tax calculations.</param>
        /// <param name="taxProfile">The tax profile for this location.</param>
        /// <exception cref="ArgumentException">Thrown when any parameter is invalid.</exception>
        public DriverSettings(
            int numPayBands,
            decimal hourlyRate,
            decimal fscRate,
            FscMode fscMode,
            decimal waitingPerMinute,
            decimal adminFee,
            string province,
            TaxProfile taxProfile)
        {
            if (numPayBands <= 0)
                throw new ArgumentException("Number of pay bands must be positive", nameof(numPayBands));
            
            if (hourlyRate < 0)
                throw new ArgumentException("Hourly rate cannot be negative", nameof(hourlyRate));
            
            if (fscRate < 0)
                throw new ArgumentException("FSC rate cannot be negative", nameof(fscRate));
            
            if (waitingPerMinute < 0)
                throw new ArgumentException("Waiting rate per minute cannot be negative", nameof(waitingPerMinute));
            
            if (adminFee < 0)
                throw new ArgumentException("Admin fee cannot be negative", nameof(adminFee));
            
            if (string.IsNullOrWhiteSpace(province))
                throw new ArgumentException("Province cannot be empty", nameof(province));

            NumPayBands = numPayBands;
            HourlyRate = hourlyRate;
            FscRate = fscRate;
            FscMode = fscMode;
            WaitingPerMinute = waitingPerMinute;
            AdminFee = adminFee;
            Province = province;
            TaxProfile = taxProfile ?? throw new ArgumentNullException(nameof(taxProfile));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is DriverSettings other)
            {
                return NumPayBands == other.NumPayBands &&
                       HourlyRate == other.HourlyRate &&
                       FscRate == other.FscRate &&
                       FscMode == other.FscMode &&
                       WaitingPerMinute == other.WaitingPerMinute &&
                       AdminFee == other.AdminFee &&
                       Province == other.Province &&
                       TaxProfile.Equals(other.TaxProfile);
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
                hash = hash * 23 + NumPayBands.GetHashCode();
                hash = hash * 23 + HourlyRate.GetHashCode();
                hash = hash * 23 + FscRate.GetHashCode();
                hash = hash * 23 + FscMode.GetHashCode();
                hash = hash * 23 + WaitingPerMinute.GetHashCode();
                hash = hash * 23 + AdminFee.GetHashCode();
                hash = hash * 23 + Province.GetHashCode();
                hash = hash * 23 + TaxProfile.GetHashCode();
                return hash;
            }
        }
    }
}
