using Cargo.Application.DTOs.Common;
using System;
using System.Collections.Generic;

namespace Cargo.Application.DTOs.DriverContracts
{
    /// <summary>
    /// Data Transfer Object for reading full driver contract details,
    /// including associated settings, rate bands, and contract period.
    /// </summary>
    public class DriverContractDto : BaseEntityDto
    {
        /// <summary>
        /// Identifier of the associated driver.
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// Full name of the driver (optional - populated when included in query).
        /// </summary>
        public string? DriverName { get; set; }

        /// <summary>
        /// Driver-specific pay and tax settings.
        /// </summary>
        public DriverSettingsDto Settings { get; set; }

        /// <summary>
        /// Applicable rate bands for this contract.
        /// </summary>
        public List<RateBandDto> RateBands { get; set; } = new();

        /// <summary>
        /// Contract effective start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Contract optional end date (null if ongoing).
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Indicates whether the contract is active at the time of retrieval.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
