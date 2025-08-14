using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.DriverVehicleAssignment
{
    /// <summary>
    /// DTO for updating an existing driver-vehicle assignment.
    /// </summary>
    public class UpdateDriverVehicleAssignmentDto
    {
        /// <summary>
        /// The unique identifier of the assignment to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Updated driver role, if applicable.
        /// </summary>
        public DriverRoleType DriverRole { get; set; }

        /// <summary>
        /// Optional: Updated end date (if closing assignment).
        /// </summary>
        public DateTime? EndedAt { get; set; }

        /// <summary>
        /// Optional: Reason for ending or cancelling the assignment.
        /// </summary>
        public string EndReason { get; set; }

        /// <summary>
        /// Optional: Updated notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Updated status.
        /// </summary>
        public AssignmentStatus Status { get; set; }
    }
}
