// ==================================================================================
// ENUM: VehicleStatus
// ==================================================================================
// Purpose: Defines the possible operational statuses for vehicles in the cargo management system
// This enum provides type-safe representation of vehicle states for better code maintainability
// ==================================================================================

namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the possible operational statuses for a vehicle
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// Vehicle is active and available for assignments
        /// </summary>
        Available = 0,

        /// <summary>
        /// Vehicle is currently assigned to a route and in transit
        /// </summary>
        InTransit = 1,

        /// <summary>
        /// Vehicle is at a maintenance facility for scheduled/unscheduled maintenance
        /// </summary>
        Maintenance = 2,

        /// <summary>
        /// Vehicle is out of service due to breakdown or damage
        /// </summary>
        OutOfService = 3,

        /// <summary>
        /// Vehicle is retired and no longer in active fleet
        /// </summary>
        Retired = 4,

        /// <summary>
        /// Vehicle is reserved for a specific assignment but not yet in transit
        /// </summary>
        Reserved = 5,

        /// <summary>
        /// Vehicle registration or permits have expired
        /// </summary>
        RegistrationExpired = 6,

        /// <summary>
        /// Vehicle is under inspection for compliance or safety checks
        /// </summary>
        UnderInspection = 7,

        /// <summary>
        /// Vehicle is parked and waiting for next assignment
        /// </summary>
        Parked = 8,

        /// <summary>
        /// Vehicle is loaded with cargo and ready for departure
        /// </summary>
        Loaded = 9,

        /// <summary>
        /// Vehicle is empty and returning to depot or next pickup location
        /// </summary>
        EmptyReturn = 10,
        /// <summary>
        /// Vehicle is Assigned to Driver
        /// </summary>
        Assigned = 11
    }
}
