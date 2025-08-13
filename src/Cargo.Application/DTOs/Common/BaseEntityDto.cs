using System;

namespace Cargo.Application.DTOs.Common
{
    /// <summary>
    /// Base DTO including common entity properties (ID and audit fields).
    /// Used for read operations to return complete entity info.
    /// </summary>
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedByIP { get; set; }
        public string? UpdatedByIP { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
