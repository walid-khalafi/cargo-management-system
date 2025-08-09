using System;
using System.Collections.Generic;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a contractual agreement between a driver and the company,
    /// including pay settings, applicable rate bands, and the contract period.
    /// This entity ties together driver-specific payroll configuration
    /// with the effective duration of the agreement.
    /// </summary>
    public class DriverContract : BaseEntity
    {
        /// <summary>
        /// EF Core constructor / serialization purposes.
        /// </summary>
        protected DriverContract() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverContract"/> class.
        /// </summary>
        /// <param name="driverId">The unique identifier of the driver.</param>
        /// <param name="settings">The driver-specific pay and tax settings.</param>
        /// <param name="rateBands">The list of rate bands applicable to this contract.</param>
        /// <param name="startDate">The start date of the contract.</param>
        /// <param name="endDate">The optional end date of the contract.</param>
        /// <exception cref="ArgumentNullException">Thrown when required parameters are null.</exception>
        public DriverContract(
            Guid driverId,
            DriverSettings settings,
            IEnumerable<RateBand> rateBands,
            DateTime startDate,
            DateTime? endDate = null)
        {
            DriverId = driverId;
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _rateBands = new List<RateBand>(rateBands ?? throw new ArgumentNullException(nameof(rateBands)));
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets the identifier of the associated driver.
        /// </summary>
        public Guid DriverId { get; private set; }

        /// <summary>
        /// Navigation property to the associated driver entity.
        /// </summary>
        public Driver Driver { get; private set; }

        /// <summary>
        /// Gets the driver-specific pay and tax settings for this contract.
        /// </summary>
        public DriverSettings Settings { get; private set; }

        private readonly List<RateBand> _rateBands = new();

        /// <summary>
        /// Gets the immutable list of rate bands that define pay rules for this contract.
        /// </summary>
        public IReadOnlyCollection<RateBand> RateBands => _rateBands.AsReadOnly();

        /// <summary>
        /// Gets the date when this contract becomes effective.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the optional date when this contract ends.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Determines whether this contract is currently active based on
        /// the start and end dates.
        /// </summary>
        public bool IsActive(DateTime? asOfDate = null)
        {
            var date = asOfDate ?? DateTime.UtcNow;
            return date >= StartDate && (EndDate == null || date <= EndDate.Value);
        }
    }
}