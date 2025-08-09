namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the status of a driver-vehicle assignment.
    /// </summary>
    public enum AssignmentStatus
    {
        /// <summary>
        /// Assignment is currently active.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Assignment is temporarily suspended.
        /// </summary>
        Suspended = 1,

        /// <summary>
        /// Assignment has been completed.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Assignment has been cancelled.
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Assignment is pending approval.
        /// </summary>
        Pending = 4
    }
}
