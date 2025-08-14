using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.VehicleOwnership
{
    /// <summary>
    /// Data Transfer Object representing a vehicle ownership record.
    /// </summary>
    public class VehicleOwnershipDto
    {
        /// <summary>
        /// Unique identifier of the ownership record.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifier of the vehicle being owned.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Identifier of the owning company.
        /// </summary>
        public Guid OwnerCompanyId { get; set; }

        /// <summary>
        /// Type of ownership (e.g., Permanent, Leased).
        /// </summary>
        public OwnershipType Type { get; set; }
        /// <summary>
        /// Start date of ownership.
        /// </summary>
        public DateTime OwnedFrom { get; set; }

        /// <summary>
        /// End date of ownership, if applicable.
        /// </summary>
        public DateTime? OwnedUntil { get; set; }

        /// <summary>
        /// Basic vehicle info (optional, for read scenarios).
        /// </summary>
        public VehicleBriefDto? Vehicle { get; set; }

        /// <summary>
        /// Basic company info (optional, for read scenarios).
        /// </summary>
        public CompanyBriefDto? OwnerCompany { get; set; }
    }

}
