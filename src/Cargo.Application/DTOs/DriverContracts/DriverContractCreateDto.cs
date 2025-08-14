using System;
using System.Collections.Generic;

namespace Cargo.Application.DTOs.DriverContracts
{
    /// <summary>
    /// DTO for creating a new driver contract.
    /// </summary>
    public class DriverContractCreateDto
    {
        /// <summary>
        /// Identifier of the driver this contract belongs to.
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// Driver-specific pay and tax settings.
        /// </summary>
        public DriverSettingsDto Settings { get; set; }

        /// <summary>
        /// List of applicable rate bands.
        /// </summary>
        public List<RateBandDto> RateBands { get; set; } = new();

        /// <summary>
        /// Contract start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Optional contract end date.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
