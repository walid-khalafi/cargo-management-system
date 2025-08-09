// ==================================================================================
// ENUM: DriverStatus
// ==================================================================================
// Purpose: Defines the possible operational statuses for drivers in the cargo management system
// This enum provides type-safe representation of driver states for better code maintainability
// ==================================================================================

namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the possible operational statuses for a driver
    /// </summary>
    public enum DriverStatus
    {
        /// <summary>
        /// Driver is active and available for assignments
        /// </summary>
        Active = 0,

        /// <summary>
        /// Driver is inactive and not available for assignments
        /// </summary>
        Inactive = 1,

        /// <summary>
        /// Driver is suspended due to violations or administrative action
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// Driver is on leave (vacation, sick leave, etc.)
        /// </summary>
        OnLeave = 3,

        /// <summary>
        /// Driver is currently on duty and assigned to a route
        /// </summary>
        OnDuty = 4,

        /// <summary>
        /// Driver is off duty but still active in the system
        /// </summary>
        OffDuty = 5,

        /// <summary>
        /// Driver's license has expired or is invalid
        /// </summary>
        LicenseExpired = 6,

        /// <summary>
        /// Driver is under review for compliance or performance issues
        /// </summary>
        UnderReview = 7
    }
}
