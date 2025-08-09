// ==================================================================================
// ENTITY: BaseEntity
// ==================================================================================
// Purpose: Base entity class that provides common properties for all entities
// This class encapsulates common audit fields and identifiers used across all entities
// ==================================================================================

using System;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Base entity class that provides common properties for all entities
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the BaseEntity class with default values
        /// </summary>
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            CreatedByIP = "System";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the user who created the entity
        /// </summary>
        public string CreatedByIP { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the user who last updated the entity
        /// </summary>
        public string? UpdatedByIP { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last updated the entity
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}
