using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.VehicleOwnership
{
    /// <summary>
    /// DTO used to create or update a vehicle ownership record.
    /// </summary>
    public class VehicleOwnershipCreateDto
    {
        /// <summary>
        /// Identifier of the vehicle being owned.
        /// </summary>
        [Required]
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Identifier of the owning company.
        /// </summary>
        [Required]
        public Guid OwnerCompanyId { get; set; }

        /// <summary>
        /// Type of ownership (e.g., Permanent, Leased).
        /// </summary>
        [Required]
        public OwnershipType Type { get; set; }

        /// <summary>
        /// Start date of ownership.
        /// </summary>
        [Required]
        public DateTime OwnedFrom { get; set; }

        /// <summary>
        /// End date of ownership, if applicable.
        /// </summary>
        public DateTime? OwnedUntil { get; set; }

    }
}
