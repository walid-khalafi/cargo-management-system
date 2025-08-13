using System;
using System.Collections.Generic;

namespace Cargo.Application.DTOs.DriverContracts
{
    /// <summary>
    /// DTO for updating an existing driver contract.
    /// Only mutable fields are included.
    /// </summary>
    public class DriverContractUpdateDto
    {
        /// <summary>
        /// Identifier of the contract being updated.
        /// </summary>
        public Guid Id { get; set; }

        public DriverSettingsDto? Settings { get; set; }
        public List<RateBandDto>? RateBands { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
