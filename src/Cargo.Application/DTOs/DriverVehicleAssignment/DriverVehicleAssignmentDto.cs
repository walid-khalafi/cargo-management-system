using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.DriverVehicleAssignment
{
    /// <summary>
    /// DTO representing a historical record of a driver-vehicle assignment.
    /// Used for transferring data without exposing the domain entity directly.
    /// </summary>
    public class DriverVehicleAssignmentDto:BaseEntityDto
    {

        /// <summary>
        /// Foreign key to the assigned driver.
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// Foreign key to the assigned vehicle.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Role of the driver in this assignment (e.g., Primary, Secondary, Backup).
        /// </summary>
        public DriverRoleType DriverRole { get; set; }

        /// <summary>
        /// UTC date and time when the assignment began.
        /// </summary>
        public DateTime AssignedAt { get; set; }

        /// <summary>
        /// UTC date and time when the assignment ended (null if ongoing).
        /// </summary>
        public DateTime? EndedAt { get; set; }

        /// <summary>
        /// Reason the assignment ended (if applicable).
        /// </summary>
        public string EndReason { get; set; }

        /// <summary>
        /// Current status of the assignment (e.g., Active, Completed, Cancelled).
        /// </summary>
        public AssignmentStatus Status { get; set; }

        /// <summary>
        /// Additional notes about the assignment.
        /// </summary>
        public string Notes { get; set; }
    }
}
