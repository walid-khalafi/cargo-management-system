namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Defines the role of a driver within a vehicle assignment.
    /// </summary>
    public enum DriverRoleType
    {
        /// <summary>
        /// Primary driver responsible for the vehicle.
        /// </summary>
        Primary = 0,

        /// <summary>
        /// Co-driver who shares driving duties with the primary driver.
        /// </summary>
        CoDriver = 1,

        /// <summary>
        /// Backup driver assigned to step in when needed.
        /// </summary>
        Backup = 2,

        /// <summary>
        /// Relief driver assigned temporarily to replace the primary driver.
        /// </summary>
        Relief = 3,

        /// <summary>
        /// Trainer responsible for instructing or supervising other drivers.
        /// </summary>
        Trainer = 4
    }
}