using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a load entry in a driver batch statement.
    /// </summary>
    public class DriverBatchLoad : BaseEntity
    {
        /// <summary>
        /// Gets or sets the DAR number.
        /// </summary>
        public string DarNumber { get; set; }

        /// <summary>
        /// Gets or sets the load number.
        /// </summary>
        public string LoadNumber { get; set; }

        /// <summary>
        /// Gets or sets the origin postal code.
        /// </summary>
        public string OriginPc { get; set; }

        /// <summary>
        /// Gets or sets the destination postal code.
        /// </summary>
        public string DestinationPc { get; set; }

        /// <summary>
        /// Gets or sets the leg miles.
        /// </summary>
        public int LegMiles { get; set; }

        /// <summary>
        /// Gets or sets the load type.
        /// </summary>
        public LoadType LoadType { get; set; }

        /// <summary>
        /// Gets or sets the rate type.
        /// </summary>
        public RateType RateType { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the band label (e.g., "51 To 100").
        /// </summary>
        public string BandLabel { get; set; }

        /// <summary>
        /// Gets or sets the base pay amount.
        /// </summary>
        public decimal BasePay { get; set; }

        /// <summary>
        /// Gets or sets the fuel surcharge pay amount.
        /// </summary>
        public decimal FscPay { get; set; }

        /// <summary>
        /// Gets or sets the temporary emergency fuel pay amount.
        /// </summary>
        public decimal TemporaryEmergencyFuelPay { get; set; }

        /// <summary>
        /// Gets or sets the net WEF pay amount.
        /// </summary>
        public decimal NetWefp { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated driver batch.
        /// </summary>
        public Guid DriverBatchId { get; set; }

        /// <summary>
        /// Gets or sets the associated driver batch.
        /// </summary>
        public DriverBatch DriverBatch { get; set; }

        /// <summary>
        /// Calculates the net pay based on rate type.
        /// </summary>
        /// <returns>The calculated net pay.</returns>
        public decimal CalculateNetPay()
        {
            switch (RateType)
            {
                case RateType.PerMile:
                    return LegMiles * Rate;
                case RateType.Flat:
                    return Rate;
                default:
                    throw new InvalidOperationException("Unknown RateType");
            }
        }
    }
}
