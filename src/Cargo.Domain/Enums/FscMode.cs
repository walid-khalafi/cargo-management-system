using System;

namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the fuel surcharge calculation mode for determining fuel surcharge rates.
    /// </summary>
    /// <remarks>
    /// Defines the different methods for calculating fuel surcharge rates in the cargo management system.
    /// </summary>
    public enum FscMode
    {
        /// <summary>
        /// No fuel surcharge is applied.
        /// </summary>
        None = 0,

        /// <summary>
        /// Fuel surcharge is calculated as a percentage of the base rate.
        /// </summary>
        Percentage = 1,

        /// <summary>
        /// Fuel surcharge is calculated as a fixed amount per unit.
        /// </summary>
        Fixed = 2,

        /// <summary>
        /// Fuel surcharge is calculated based on a sliding scale.
        /// </summary>
        SlidingScale = 3,

        /// <summary>
        /// Fuel surcharge is calculated based on a custom formula.
        /// </summary>
        Custom = 4
    }
}
