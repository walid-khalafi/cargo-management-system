using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a rate band for pay calculations based on mileage and load type.
    /// This value object encapsulates the rate structure for different mileage ranges and load types.
    /// </summary>
    public sealed class RateBand
    {
        /// <summary>
        /// Gets the band identifier or sequence number.
        /// </summary>
        public int Band { get; }

        /// <summary>
        /// Gets the minimum mileage for this band.
        /// </summary>
        public decimal MinMiles { get; }

        /// <summary>
        /// Gets the maximum mileage for this band.
        /// </summary>
        public decimal MaxMiles { get; }

        /// <summary>
        /// Gets the band name (FLAT or Per Mile).
        /// </summary>
        public string BandName { get; }

        /// <summary>
        /// Gets the load type (Container or Flatbed).
        /// </summary>
        public LoadType LoadType { get; }

        /// <summary>
        /// Gets the container rate for this band.
        /// </summary>
        public decimal ContainerRate { get; }

        /// <summary>
        /// Gets the flatbed rate for this band.
        /// </summary>
        public decimal FlatbedRate { get; }

        /// <summary>
        /// Initializes a new instance of the RateBand class.
        /// </summary>
        /// <param name="band">The band identifier.</param>
        /// <param name="minMiles">The minimum mileage for this band.</param>
        /// <param name="maxMiles">The maximum mileage for this band.</param>
        /// <param name="bandName">The band name (FLAT or Per Mile).</param>
        /// <param name="loadType">The load type (Container or Flatbed).</param>
        /// <param name="containerRate">The container rate for this band.</param>
        /// <param name="flatbedRate">The flatbed rate for this band.</param>
        /// <exception cref="ArgumentException">Thrown when any parameter is invalid.</exception>
        public RateBand(
            int band,
            decimal minMiles,
            decimal maxMiles,
            string bandName,
            LoadType loadType,
            decimal containerRate,
            decimal flatbedRate)
        {
            if (band <= 0)
                throw new ArgumentException("Band must be positive", nameof(band));
            
            if (minMiles < 0)
                throw new ArgumentException("Minimum miles cannot be negative", nameof(minMiles));
            
            if (maxMiles < minMiles)
                throw new ArgumentException("Maximum miles must be greater than or equal to minimum miles", nameof(maxMiles));
            
            if (string.IsNullOrWhiteSpace(bandName))
                throw new ArgumentException("Band name cannot be empty", nameof(bandName));
            
            if (containerRate < 0)
                throw new ArgumentException("Container rate cannot be negative", nameof(containerRate));
            
            if (flatbedRate < 0)
                throw new ArgumentException("Flatbed rate cannot be negative", nameof(flatbedRate));

            Band = band;
            MinMiles = minMiles;
            MaxMiles = maxMiles;
            BandName = bandName;
            LoadType = loadType;
            ContainerRate = containerRate;
            FlatbedRate = flatbedRate;
        }

        /// <summary>
        /// Calculates the base pay for a given load type and mileage.
        /// </summary>
        /// <param name="loadType">The type of load.</param>
        /// <param name="miles">The mileage to calculate pay for.</param>
        /// <returns>The calculated base pay amount.</returns>
        public decimal GetBasePay(LoadType loadType, decimal miles)
        {
            if (miles < MinMiles || miles > MaxMiles)
                return 0;

            var rate = loadType == LoadType.Container ? ContainerRate : FlatbedRate;
            
            if (BandName.Equals("FLAT", StringComparison.OrdinalIgnoreCase))
                return rate;
            
            return Math.Round(miles * rate, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is RateBand other)
            {
                return Band == other.Band &&
                       MinMiles == other.MinMiles &&
                       MaxMiles == other.MaxMiles &&
                       BandName == other.BandName &&
                       LoadType == other.LoadType &&
                       ContainerRate == other.ContainerRate &&
                       FlatbedRate == other.FlatbedRate;
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
                hash = hash * 23 + Band.GetHashCode();
                hash = hash * 23 + MinMiles.GetHashCode();
                hash = hash * 23 + MaxMiles.GetHashCode();
                hash = hash * 23 + BandName.GetHashCode();
                hash = hash * 23 + LoadType.GetHashCode();
                hash = hash * 23 + ContainerRate.GetHashCode();
                hash = hash * 23 + FlatbedRate.GetHashCode();
                return hash;
            }
        }
    }
}
