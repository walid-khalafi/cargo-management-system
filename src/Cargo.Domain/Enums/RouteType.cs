namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Specifies the general classification of a route
    /// based on road type or operational characteristics.
    /// </summary>
    public enum RouteType
    {
        /// <summary>
        /// A route that primarily uses highways or expressways.
        /// </summary>
        Highway = 0,

        /// <summary>
        /// A route that primarily uses local or urban roads.
        /// </summary>
        Local = 1,

        /// <summary>
        /// A route that combines both highway and local road segments.
        /// </summary>
        Mixed = 2,

        /// <summary>
        /// Any other route type not covered by the predefined categories.
        /// </summary>
        Other = 3
    }
}