using System;
using Cargo.Domain.Enums;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents the assignment of a driver to a vehicle in the cargo management system.
    /// Tracks history with assignment dates, roles, status, and optional end reason.
    /// Acts as a historical record rather than overwriting current assignments.
    /// </summary>
    public class DriverVehicleAssignment : BaseEntity
    {
        /// <summary>
        /// Unique identifier for this assignment record.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Foreign key to the assigned driver.
        /// </summary>
        public Guid DriverId { get; private set; }

        /// <summary>
        /// Navigation property to the assigned driver.
        /// </summary>
        public virtual Driver Driver { get; private set; }

        /// <summary>
        /// Foreign key to the assigned vehicle.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Navigation property to the assigned vehicle.
        /// </summary>
        public virtual Vehicle Vehicle { get; private set; }

        /// <summary>
        /// Role of the driver in this assignment (e.g., Primary, Secondary, Backup).
        /// </summary>
        public DriverRoleType DriverRole { get; private set; }

        /// <summary>
        /// UTC date and time when the assignment began.
        /// </summary>
        public DateTime AssignedAt { get; private set; }

        /// <summary>
        /// UTC date and time when the assignment ended (null if ongoing).
        /// </summary>
        public DateTime? EndedAt { get; private set; }

        /// <summary>
        /// Reason the assignment ended (if applicable).
        /// </summary>
        public string? EndReason { get; private set; }

        /// <summary>
        /// Current status of the assignment (e.g., Active, Completed, Cancelled).
        /// </summary>
        public AssignmentStatus Status { get; private set; }

        /// <summary>
        /// Additional notes about the assignment.
        /// </summary>
        public string Notes { get; private set; }

        private DriverVehicleAssignment() { } // EF Core

        /// <summary>
        /// Creates a new driver-vehicle assignment record.
        /// </summary>
        public DriverVehicleAssignment(
            Guid driverId,
            Guid vehicleId,
            DriverRoleType driverRole,
            string notes = null)
        {
            if (driverId == Guid.Empty) throw new ArgumentException("Driver ID cannot be empty.", nameof(driverId));
            if (vehicleId == Guid.Empty) throw new ArgumentException("Vehicle ID cannot be empty.", nameof(vehicleId));
          

            Id = Guid.NewGuid();
            DriverId = driverId;
            VehicleId = vehicleId;
            DriverRole = driverRole;
            Notes = notes;

            AssignedAt = DateTime.UtcNow;
            Status = AssignmentStatus.Active;
        }

        /// <summary>
        /// Returns true if the assignment is active and not ended.
        /// </summary>
        public bool IsActive() => Status == AssignmentStatus.Active && !EndedAt.HasValue;

        /// <summary>
        /// Returns true if the assignment has an end date.
        /// </summary>
        public bool IsCompleted() => EndedAt.HasValue;

        /// <summary>
        /// Ends the assignment, providing a reason.
        /// </summary>
        public void EndAssignment(string reason)
        {
            if (!IsActive())
                throw new InvalidOperationException("Cannot end an assignment that is not active.");

            EndedAt = DateTime.UtcNow;
            EndReason = reason;
            Status = AssignmentStatus.Completed;
        }

        /// <summary>
        /// Cancels the assignment before it starts or without completion.
        /// </summary>
        public void CancelAssignment(string reason)
        {
            if (!IsActive())
                throw new InvalidOperationException("Cannot cancel an assignment that is not active.");

            EndedAt = DateTime.UtcNow;
            EndReason = reason;
            Status = AssignmentStatus.Cancelled;
        }

        /// <summary>
        /// Returns the total duration of the assignment.
        /// </summary>
        public TimeSpan GetAssignmentDuration()
        {
            var end = EndedAt ?? DateTime.UtcNow;
            return end - AssignedAt;
        }
    }
}