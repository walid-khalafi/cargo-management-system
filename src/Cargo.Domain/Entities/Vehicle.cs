using System;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle in the cargo management system
    /// This entity encapsulates all vehicle-related information including specifications,
    /// registration details, and operational status
    /// </summary>
    public class Vehicle : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the Vehicle class with default values
        /// </summary>
        public Vehicle()
        {
            Status = VehicleStatus.Available;
            IsAvailable = true;
        }

        /// <summary>
        /// Gets or sets the vehicle's make
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's VIN (Vehicle Identification Number)
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's registration number
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's current status
        /// </summary>
        public VehicleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's availability status
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's capacity in tons
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's plate number
        /// </summary>
        public PlateNumber PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's fuel type
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's current mileage
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's current location
        /// </summary>
        public string CurrentLocation { get; set; }

        /// <summary>
        /// Gets or sets the vehicle's current driver ID
        /// </summary>
        public Guid? DriverId { get; set; }

        /// <summary>
        /// Gets or sets the owner company identifier.
        /// </summary>
        public Guid OwnerCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the owner company.
        /// </summary>
        public virtual Company OwnerCompany { get; set; }

        /// <summary>
        /// Determines if the vehicle is currently available for assignments
        /// </summary>
        /// <returns>True if vehicle is available, false otherwise</returns>
        public bool IsAvailableForAssignment()
        {
            return IsAvailable && Status == VehicleStatus.Available;
        }
    }
}
