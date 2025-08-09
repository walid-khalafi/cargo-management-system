namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Defines the type or classification of a location
    /// within the logistics and cargo management domain.
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// A storage or distribution warehouse.
        /// </summary>
        Warehouse = 0,

        /// <summary>
        /// A central depot or transportation hub.
        /// </summary>
        Depot = 1,

        /// <summary>
        /// A customer pickup or delivery point.
        /// </summary>
        Customer = 2,

        /// <summary>
        /// A commercial or industrial port.
        /// </summary>
        Port = 3,

        /// <summary>
        /// A designated border crossing facility.
        /// </summary>
        BorderCrossing = 4,

        /// <summary>
        /// A fuel station or refuelling point.
        /// </summary>
        FuelStation = 5,

        /// <summary>
        /// Any other location type not explicitly listed.
        /// </summary>
        Other = 99
    }
}