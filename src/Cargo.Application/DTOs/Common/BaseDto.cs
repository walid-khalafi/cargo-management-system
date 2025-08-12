using System;

namespace Cargo.Application.DTOs.Common
{
    /// <summary>
    /// Serves as the base class for all Data Transfer Objects (DTOs) in the Cargo Management System.
    /// Provides common audit-related properties to ensure consistency across different DTO types.
    /// </summary>
    /// <remarks>
    /// Inherit from <see cref="BaseDto"/> whenever a DTO requires a unique identifier and 
    /// audit metadata such as creation and update timestamps.
    /// This approach enforces uniformity and reduces repetitive code.
    /// </remarks>
    public abstract class BaseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the DTO instance.
        /// Typically represented as a GUID to ensure global uniqueness.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the DTO was created (in UTC).
        /// Automatically set when the entity is first persisted.
        /// </summary>
        /// <example>2025-08-12T12:00:00Z</example>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the DTO was last updated (in UTC).
        /// Nullable to allow tracking entities that have never been updated.
        /// </summary>
        /// <example>2025-08-15T15:45:00Z</example>
        public DateTime? UpdatedAt { get; set; }
    }
}
