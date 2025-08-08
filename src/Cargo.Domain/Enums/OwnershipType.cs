namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the type of vehicle ownership.
    /// </summary>
    public enum OwnershipType
    {
        /// <summary>
        /// Vehicle is owned by the fleet company.
        /// </summary>
        OwnedByFleet = 0,

        /// <summary>
        /// Vehicle is owned by the driver or driver's company.
        /// </summary>
        OwnedByDriverCompany = 1
    }
}
