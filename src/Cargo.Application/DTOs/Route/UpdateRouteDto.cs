namespace Cargo.Application.DTOs.Route
{
    /// <summary>
    /// Represents the required data for updating an existing route.
    /// </summary>
    /// <remarks>
    /// Inherits base route creation fields and adds ID, status, and assigned driver/vehicle identifiers.
    /// </remarks>
    public class UpdateRouteDto : CreateRouteDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the route being updated.
        /// </summary>
        /// <example>88b4f3ae-3421-4f38-9ab2-c88158f38e4e</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the current operational status of the route.
        /// </summary>
        /// <example>In Progress</example>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the assigned driver.
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public Guid? DriverId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the assigned vehicle.
        /// </summary>
        /// <example>5dfe12a6-8129-44b9-a2a1-92b9cebc246b</example>
        public Guid? VehicleId { get; set; }
    }

}
