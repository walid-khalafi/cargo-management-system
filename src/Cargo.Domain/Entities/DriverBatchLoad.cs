using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents an individual load entry for a driver within a batch payroll statement.
    /// This entity captures mileage, pay rates, calculated pay components,
    /// and references to associated batch data.
    /// </summary>
    public class DriverBatchLoad : BaseEntity
    {
        #region Invoice & Dispatch Identifiers

        /// <summary>
        /// Gets the DAR (Dispatch and Routing) reference number for this load.
        /// </summary>
        public string DarNumber { get; private set; }

        /// <summary>
        /// Gets the unique load number assigned to this trip.
        /// </summary>
        public string LoadNumber { get; private set; }

        #endregion

        #region Routing Information

        /// <summary>
        /// Gets the origin postal/zip code for this load.
        /// </summary>
        public string OriginPc { get; private set; }

        /// <summary>
        /// Gets the destination postal/zip code for this load.
        /// </summary>
        public string DestinationPc { get; private set; }

        #endregion

        #region Mileage & Load Type

        /// <summary>
        /// Gets the distance covered for this leg of the trip in miles.
        /// </summary>
        public int LegMiles { get; private set; }

        /// <summary>
        /// Gets the classification of the load (e.g., linehaul, local).
        /// </summary>
        public LoadType LoadType { get; private set; }

        /// <summary>
        /// Gets the type of rate applied for pay calculation (per-mile, flat rate, etc.).
        /// </summary>
        public RateType RateType { get; private set; }

        #endregion

        #region Rate Details

        /// <summary>
        /// Gets the rate amount used for pay calculation.
        /// This may represent a per-mile rate or a flat trip rate depending on <see cref="RateType"/>.
        /// </summary>
        public decimal Rate { get; private set; }

        /// <summary>
        /// Gets the descriptive label for the rate band applied (e.g., "Band A").
        /// </summary>
        public string BandLabel { get; private set; }

        #endregion

        #region Pay Breakdown

        /// <summary>
        /// Gets the calculated base pay for this load.
        /// </summary>
        public decimal BasePay { get; private set; }

        /// <summary>
        /// Gets the Fuel Surcharge (FSC) pay amount for this load.
        /// </summary>
        public decimal FscPay { get; private set; }

        /// <summary>
        /// Gets the temporary emergency fuel pay amount for this load.
        /// </summary>
        public decimal TemporaryEmergencyFuelPay { get; private set; }

        /// <summary>
        /// Gets the total pay including base, FSC, and temporary emergency fuel pay.
        /// </summary>
        public decimal NetWefp { get; private set; }

        #endregion

        #region Relationships

        /// <summary>
        /// Gets the foreign key reference to the owning <see cref="DriverBatch"/>.
        /// </summary>
        public Guid DriverBatchId { get; private set; }

        /// <summary>
        /// Gets the associated <see cref="DriverBatch"/> entity.
        /// </summary>
        public virtual DriverBatch DriverBatch { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new load entry in a driver batch statement, initializing rate and pay calculations.
        /// </summary>
        /// <param name="darNumber">The DAR reference number.</param>
        /// <param name="loadNumber">The load number (required).</param>
        /// <param name="originPc">Origin postal/zip code.</param>
        /// <param name="destinationPc">Destination postal/zip code.</param>
        /// <param name="legMiles">The leg distance in miles.</param>
        /// <param name="loadType">The classification of the load.</param>
        /// <param name="rateType">The type of rate applied for pay calculation.</param>
        /// <param name="rate">The pay rate value.</param>
        /// <param name="bandLabel">The descriptive band label applied.</param>
        /// <param name="fscPay">Fuel surcharge pay amount.</param>
        /// <param name="temporaryEmergencyFuelPay">Temporary emergency fuel pay amount.</param>
        /// <exception cref="ArgumentException">Thrown when required parameters are missing or invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when numeric values are negative.</exception>
        public DriverBatchLoad(
            string darNumber,
            string loadNumber,
            string originPc,
            string destinationPc,
            int legMiles,
            LoadType loadType,
            RateType rateType,
            decimal rate,
            string bandLabel,
            decimal fscPay,
            decimal temporaryEmergencyFuelPay)
        {
            if (string.IsNullOrWhiteSpace(loadNumber))
                throw new ArgumentException("Load number is required.", nameof(loadNumber));
            if (legMiles < 0)
                throw new ArgumentOutOfRangeException(nameof(legMiles), "Miles cannot be negative.");
            if (rate < 0)
                throw new ArgumentOutOfRangeException(nameof(rate), "Rate cannot be negative.");

            DarNumber = darNumber;
            LoadNumber = loadNumber;
            OriginPc = originPc;
            DestinationPc = destinationPc;
            LegMiles = legMiles;
            LoadType = loadType;
            RateType = rateType;
            Rate = rate;
            BandLabel = bandLabel;

            BasePay = CalculateBasePay();
            FscPay = Round2(fscPay);
            TemporaryEmergencyFuelPay = Round2(temporaryEmergencyFuelPay);
            NetWefp = Round2(BasePay + FscPay + TemporaryEmergencyFuelPay);
        }

        #endregion

        #region Business Logic

        /// <summary>
        /// Calculates the base pay according to the <see cref="RateType"/> and <see cref="LegMiles"/>.
        /// </summary>
        private decimal CalculateBasePay()
        {
            return RateType switch
            {
                RateType.PerMile => Round2(LegMiles * Rate),
                RateType.Flat => Round2(Rate),
                _ => throw new InvalidOperationException($"Unknown RateType: {RateType}")
            };
        }

        /// <summary>
        /// Calculates the total net pay for this load.
        /// </summary>
        public decimal CalculateNetPay() => NetWefp; // precomputed in constructor

        /// <summary>
        /// Rounds a decimal value to 2 decimal places, using AwayFromZero midpoint rounding.
        /// </summary>
        private static decimal Round2(decimal value) =>
            Math.Round(value, 2, MidpointRounding.AwayFromZero);

        #endregion
    }
}