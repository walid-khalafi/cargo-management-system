// ==================================================================================
// ENTITY: DriverVehicleAssignment
// ==================================================================================
// Purpose: Represents the assignment relationship between drivers and vehicles
// This entity tracks which drivers are assigned to which vehicles, including
// assignment dates, roles, and status information
// ==================================================================================

using System;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a driver-vehicle assignment in the cargo management system
    /// This entity manages the many-to-many relationship between drivers and vehicles
    /// with additional assignment details
    /// </summary>
    public class DriverVehicleAssignment : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the DriverVehicleAssignment class
        /// </summary>
        public DriverVehicleAssignment()
        {
            Id = Guid.NewGuid();
            AssignedAt = DateTime.UtcNow;
            Status = AssignmentStatus.Active;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the assignment
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the driver
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// Gets or sets the driver assigned to the vehicle
        /// </summary>
        public virtual Driver Driver { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the vehicle
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle being assigned to the driver
        /// </summary>
        public virtual Vehicle Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the role of the driver for this assignment (Primary, Secondary, Backup, etc.)
        /// </summary>
        public string DriverRole { get; set; }

        /// <summary>
        /// Gets or sets the date when the assignment was made
        /// </summary>
        public DateTime AssignedAt { get; set; }

        /// <summary>
        /// Gets or sets the date when the assignment ends (null for ongoing assignments)
        /// </summary>
        public DateTime? EndedAt { get; set; }

        /// <summary>
        /// Gets or sets the reason for ending the assignment
        /// </summary>
        public string EndReason { get; set; }

        /// <summary>
        /// Gets or sets the assignment status
        /// </summary>
        public AssignmentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets any additional notes about the assignment
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Determines if the assignment is currently active
        /// </summary>
        /// <returns>True if the assignment is active and not ended</returns>
        public bool IsActive()
        {
            return Status == AssignmentStatus.Active && !EndedAt.HasValue;
        }

        /// <summary>
        /// Determines if the assignment has been completed
        /// </summary>
        /// <returns>True if the assignment has an end date</returns>
        public bool IsCompleted()
        {
            return EndedAt.HasValue;
        }

        /// <summary>
        /// Calculates the duration of the assignment
        /// </summary>
        /// <returns>TimeSpan representing the assignment duration</returns>
        public TimeSpan GetAssignmentDuration()
        {
            if (!EndedAt.HasValue)
                return DateTime.UtcNow - AssignedAt;

            return EndedAt.Value - AssignedAt;
        }
    }


}
