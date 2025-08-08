// ==================================================================================
// ENUM: RouteStatus
// ==================================================================================
// Purpose: Defines the possible operational statuses for routes in the cargo management system
// This enum provides type-safe representation of route states for better code maintainability
// ==================================================================================

namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the possible operational statuses for a route
    /// </summary>
    public enum RouteStatus
    {
        /// <summary>
        /// Route is active and available for vehicle assignments
        /// </summary>
        Active = 0,

        /// <summary>
        /// Route is inactive and not available for new assignments
        /// </summary>
        Inactive = 1,

        /// <summary>
        /// Route is archived and no longer in active use
        /// </summary>
        Archived = 2,

        /// <summary>
        /// Route is under review for approval or modification
        /// </summary>
        UnderReview = 3,

        /// <summary>
        /// Route is temporarily suspended due to external factors (weather, road conditions, etc.)
        /// </summary>
        Suspended = 4,

        /// <summary>
        /// Route is permanently discontinued and should not be used
        /// </summary>
        Discontinued = 5,

        /// <summary>
        /// Route is in draft status and not yet ready for use
        /// </summary>
        Draft = 6,

        /// <summary>
        /// Route is being updated or modified
        /// </summary>
        Updating = 7
    }
}
