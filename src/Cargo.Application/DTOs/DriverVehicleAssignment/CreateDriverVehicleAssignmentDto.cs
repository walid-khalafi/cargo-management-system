using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.DriverVehicleAssignment
{
    /// <summary>
    /// DTO for creating a new driver-vehicle assignment.
    /// </summary>
    public class CreateDriverVehicleAssignmentDto
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
        /// Role of the driver in this assignment.
        /// </summary>
        public DriverRoleType DriverRole { get; set; }

        /// <summary>
        /// Optional notes.
        /// </summary>
        public string Notes { get; set; }
    }
}
