using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents the ownership record of a vehicle by a company.
    /// </summary>
    public class VehicleOwnership : BaseEntity
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the owner company identifier.
        /// </summary>
        public Guid OwnerCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the type of ownership.
        /// </summary>
        public OwnershipType Type { get; set; }

        /// <summary>
        /// Gets or sets the date from which the vehicle is owned.
        /// </summary>
        public DateTime OwnedFrom { get; set; }

        /// <summary>
        /// Gets or sets the date until which the vehicle is owned (nullable).
        /// </summary>
        public DateTime? OwnedUntil { get; set; }

        /// <summary>
        /// Gets or sets the associated vehicle.
        /// </summary>
        public virtual Vehicle Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the owner company.
        /// </summary>
        public virtual Company OwnerCompany { get; set; }
    }
}
