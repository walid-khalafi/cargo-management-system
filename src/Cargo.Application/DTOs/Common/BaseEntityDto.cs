using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Common
{
    /// <summary>
    /// Data Transfer Object representing the common base properties shared by all entities.
    /// This DTO is typically used as a base class for other DTOs to inherit
    /// audit and identification fields.
    /// </summary>
    public class BaseEntityDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time (in UTC) when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time (in UTC) when the entity was last updated, if applicable.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the IP address from which the entity creation was performed.
        /// </summary>
        public string CreatedByIP { get; set; }

        /// <summary>
        /// Gets or sets the IP address from which the entity update was performed.
        /// May be <c>null</c> if no update has occurred.
        /// </summary>
        public string? UpdatedByIP { get; set; }

        /// <summary>
        /// Gets or sets the identifier (username, email, or system user ID)
        /// of the user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier (username, email, or system user ID)
        /// of the user who last updated the entity.
        /// May be <c>null</c> if no update has occurred.
        /// </summary>
        public string? UpdatedBy { get; set; }

    }
}
