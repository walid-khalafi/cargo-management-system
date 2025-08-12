namespace Cargo.Application.DTOs.Driver
{
    /// <summary>
    /// Represents the data required to update an existing driver record.
    /// Inherits fields from <see cref="CreateDriverDto"/> and adds status and identifier.
    /// </summary>
    /// <remarks>
    /// Used for HTTP PUT or PATCH requests when modifying driver information.
    /// </remarks>
    public class UpdateDriverDto : CreateDriverDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the driver to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the current employment status of the driver.
        /// </summary>
        /// <example>Inactive</example>
        public string Status { get; set; } = string.Empty;
    }
}
