using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Company
{
    /// <summary>
    /// Represents summarized company information for list or overview displays.
    /// </summary>
    /// <remarks>
    /// This DTO is optimized for grid or table listings and contains only the most essential fields.
    /// </remarks>
    public class CompanySummaryDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the registered legal name of the company.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the official registration or business number.
        /// </summary>
        public string RegistrationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city in which the company is located.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the province or region of the company's primary location.
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total number of active drivers associated with the company.
        /// </summary>
        public int DriverCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of vehicles owned or operated by the company.
        /// </summary>
        public int VehicleCount { get; set; }
    }
}
