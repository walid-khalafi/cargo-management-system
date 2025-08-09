using System;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle in the cargo management system.
    /// Contains all technical specifications, registration details,
    /// ownership information, and operational status.
    /// </summary>
    public class Vehicle : BaseEntity
    {
        // --- Basic specifications ---

        /// <summary>
        /// Manufacturer/brand of the vehicle.
        /// </summary>
        public string Make { get; private set; }

        /// <summary>
        /// Model name of the vehicle.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Manufacturing year of the vehicle.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Color of the vehicle.
        /// </summary>
        public string Color { get; private set; }

        /// <summary>
        /// Vehicle Identification Number (VIN).
        /// Unique global identifier for this vehicle.
        /// </summary>
        public string VIN { get; private set; }

        /// <summary>
        /// Vehicle's registration number.
        /// </summary>
        public string RegistrationNumber { get; private set; }

        /// <summary>
        /// Official license plate number.
        /// </summary>
        public PlateNumber PlateNumber { get; private set; }

        /// <summary>
        /// Fuel type used by the vehicle (e.g. Diesel, Gasoline, Electric).
        /// </summary>
        public string FuelType { get; private set; }

        /// <summary>
        /// Cargo capacity of the vehicle (in tons).
        /// </summary>
        public int Capacity { get; private set; }

        // --- Operational status ---

        /// <summary>
        /// Current operational status of the vehicle.
        /// </summary>
        public VehicleStatus Status { get; private set; }

        /// <summary>
        /// Indicates whether the vehicle is currently available for assignments.
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Current total mileage on the vehicle.
        /// </summary>
        public int Mileage { get; private set; }

        /// <summary>
        /// Current physical location of the vehicle.
        /// </summary>
        public string CurrentLocation { get; private set; }

        // --- Ownership and assignment ---

        /// <summary>
        /// Identifier of the driver currently assigned to this vehicle (if any).
        /// </summary>
        public Guid? DriverId { get; private set; }

        /// <summary>
        /// Identifier of the company that owns this vehicle.
        /// </summary>
        public Guid OwnerCompanyId { get; private set; }

        /// <summary>
        /// Reference to the owning company entity.
        /// </summary>
        public virtual Company OwnerCompany { get; private set; }

        private Vehicle() { } // EF Core constructor

        /// <summary>
        /// Creates a new vehicle record with the specified specifications and ownership.
        /// </summary>
        public Vehicle(
            string make,
            string model,
            int year,
            string color,
            string vin,
            string registrationNumber,
            PlateNumber plateNumber,
            string fuelType,
            int capacity,
            Guid ownerCompanyId)
        {
            if (string.IsNullOrWhiteSpace(make)) throw new ArgumentException("Make is required.", nameof(make));
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model is required.", nameof(model));
            if (year < 1980 || year > DateTime.UtcNow.Year + 1) throw new ArgumentOutOfRangeException(nameof(year), "Invalid year.");
            if (string.IsNullOrWhiteSpace(vin)) throw new ArgumentException("VIN is required.", nameof(vin));
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));

            Make = make;
            Model = model;
            Year = year;
            Color = color;
            VIN = vin;
            RegistrationNumber = registrationNumber;
            PlateNumber = plateNumber ?? throw new ArgumentNullException(nameof(plateNumber));
            FuelType = fuelType;
            Capacity = capacity;
            OwnerCompanyId = ownerCompanyId;

            Status = VehicleStatus.Available;
            IsAvailable = true;
            Mileage = 0;
        }

        // --- Domain behaviors ---

        /// <summary>
        /// Determines whether the vehicle is ready to be assigned to a driver.
        /// </summary>
        public bool IsAvailableForAssignment() =>
            IsAvailable && Status == VehicleStatus.Available;

        /// <summary>
        /// Assigns the specified driver to this vehicle.
        /// </summary>
        public void AssignDriver(Guid driverId)
        {
            if (!IsAvailableForAssignment())
                throw new InvalidOperationException("Vehicle is not available for assignment.");
            DriverId = driverId;
            IsAvailable = false;
            Status = VehicleStatus.Assigned;
        }

        /// <summary>
        /// Unassigns the current driver from the vehicle, making it available again.
        /// </summary>
        public void UnassignDriver()
        {
            DriverId = null;
            IsAvailable = true;
            Status = VehicleStatus.Available;
        }

        /// <summary>
        /// Updates the current physical location of the vehicle.
        /// </summary>
        public void UpdateLocation(string location) =>
            CurrentLocation = location;

        /// <summary>
        /// Increases the vehicle's recorded mileage.
        /// </summary>
        public void AddMileage(int additionalMiles)
        {
            if (additionalMiles < 0) throw new ArgumentOutOfRangeException(nameof(additionalMiles));
            Mileage += additionalMiles;
        }

        /// <summary>
        /// Marks the vehicle as sent to maintenance and unavailable for assignments.
        /// </summary>
        public void SendToMaintenance()
        {
            Status = VehicleStatus.Maintenance;
            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as available for new assignments.
        /// </summary>
        public void MarkAsAvailable()
        {
            Status = VehicleStatus.Available;
            IsAvailable = true;
        }
    }
}