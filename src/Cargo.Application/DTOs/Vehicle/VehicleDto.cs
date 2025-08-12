using System;
using Cargo.Application.DTOs.Common;

namespace Cargo.Application.DTOs.Vehicle
{
/// <summary>
/// Represents detailed vehicle information in the cargo management system.
/// </summary>
/// <remarks>
/// Inherits from <see cref="BaseDto"/> to include unique identifier and audit metadata.
/// Contains general specifications, registration details, operational status, and assignment information.
/// </remarks>
public class VehicleDto : BaseDto
{
    /// <summary>
    /// Gets or sets the manufacturer of the vehicle.
    /// </summary>
    /// <example>Volvo</example>
    public string Make { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the specific model name of the vehicle.
    /// </summary>
    /// <example>FH16</example>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the manufacturing year of the vehicle.
    /// </summary>
    /// <example>2022</example>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the exterior color of the vehicle.
    /// </summary>
    /// <example>White</example>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Vehicle Identification Number (VIN) assigned by the manufacturer.
    /// </summary>
    /// <example>1M8GDM9AXKP042788</example>
    public string VIN { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the official registration number issued by local authorities.
    /// </summary>
    /// <example>REG-987654</example>
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the license plate details for the vehicle.
    /// </summary>
    public PlateNumberDto PlateNumber { get; set; } = new PlateNumberDto();

    /// <summary>
    /// Gets or sets the type of fuel used by the vehicle (e.g., Diesel, Petrol, Electric).
    /// </summary>
    /// <example>Diesel</example>
    public string FuelType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum load or passenger capacity of the vehicle.
    /// Measured in kilograms for cargo or number of seats for passenger vehicles.
    /// </summary>
    /// <example>20000</example>
    public int Capacity { get; set; }

    /// <summary>
    /// Gets or sets the current operational status of the vehicle.
    /// Common values: Active, Under Maintenance, Decommissioned.
    /// </summary>
    /// <example>Active</example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the vehicle is currently available for assignment.
    /// </summary>
    /// <example>true</example>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets the total distance the vehicle has traveled, measured in kilometers.
    /// </summary>
    /// <example>45000</example>
    public int Mileage { get; set; }

    /// <summary>
    /// Gets or sets the current physical location of the vehicle.
    /// </summary>
    /// <example>Warehouse A - Dubai</example>
    public string CurrentLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the currently assigned driver, if applicable.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid? DriverId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the company that owns the vehicle.
    /// </summary>
    /// <example>ac2f0ef3-1620-4dd7-b5f9-a8972c4b9a73</example>
    public Guid OwnerCompanyId { get; set; }
}


/// <summary>
/// Represents the data required to create a new vehicle record in the system.
/// </summary>
/// <remarks>
/// Used in HTTP POST requests to register a new vehicle under a specific company.
/// Contains general specifications, registration details, and ownership information.
/// </remarks>
public class CreateVehicleDto
{
    /// <summary>
    /// Gets or sets the manufacturer (brand) of the vehicle.
    /// </summary>
    /// <example>Volvo</example>
    public string Make { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the specific model of the vehicle.
    /// </summary>
    /// <example>FH16</example>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the manufacturing year of the vehicle.
    /// </summary>
    /// <example>2022</example>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the exterior color of the vehicle.
    /// </summary>
    /// <example>White</example>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Vehicle Identification Number (VIN) assigned by the manufacturer.
    /// </summary>
    /// <example>1M8GDM9AXKP042788</example>
    public string VIN { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the official registration number issued by local authorities.
    /// </summary>
    /// <example>REG-987654</example>
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the license plate details for the vehicle.
    /// </summary>
    public PlateNumberDto PlateNumber { get; set; } = new PlateNumberDto();

    /// <summary>
    /// Gets or sets the type of fuel used by the vehicle (e.g., Diesel, Petrol, Electric).
    /// </summary>
    /// <example>Diesel</example>
    public string FuelType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum load or passenger capacity of the vehicle.
    /// Measured in kilograms for cargo vehicles or seat count for passenger vehicles.
    /// </summary>
    /// <example>20000</example>
    public int Capacity { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the company that owns this vehicle.
    /// </summary>
    /// <example>ac2f0ef3-1620-4dd7-b5f9-a8972c4b9a73</example>
    public Guid OwnerCompanyId { get; set; }
}


/// <summary>
/// Represents the data required to update an existing vehicle record.
/// </summary>
/// <remarks>
/// Inherits base properties from <see cref="CreateVehicleDto"/> (such as make, model, VIN, and capacity)
/// and adds fields for identification, operational status, availability, mileage, location, and driver assignment.
/// </remarks>
public class UpdateVehicleDto : CreateVehicleDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the vehicle being updated.
    /// </summary>
    /// <example>8d92e3ff-39ec-4d64-9476-5648f3d8c5d2</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the current operational status of the vehicle.
    /// Common values include Active, In Maintenance, or Decommissioned.
    /// </summary>
    /// <example>Active</example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the vehicle is currently available for new assignments.
    /// </summary>
    /// <example>true</example>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets the total distance the vehicle has traveled, in kilometers.
    /// </summary>
    /// <example>45200</example>
    public int Mileage { get; set; }

    /// <summary>
    /// Gets or sets the current known physical location of the vehicle.
    /// </summary>
    /// <example>Warehouse A - Dubai</example>
    public string CurrentLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier of the driver currently assigned to the vehicle, if any.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid? DriverId { get; set; }
}


   /// <summary>
/// Represents summarized vehicle information for list or overview displays.
/// </summary>
/// <remarks>
/// Used in vehicle listings, dashboards, or search results.
/// Provides high-level details without full technical or assignment history.
/// </remarks>
public class VehicleSummaryDto : BaseDto
{
    /// <summary>
    /// Gets or sets the manufacturer (brand) of the vehicle.
    /// </summary>
    /// <example>Volvo</example>
    public string Make { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the specific model of the vehicle.
    /// </summary>
    /// <example>FH16</example>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the manufacturing year of the vehicle.
    /// </summary>
    /// <example>2022</example>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the official registration number issued by authorities.
    /// </summary>
    /// <example>REG-987654</example>
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the vehicle's license plate number (as a string for display purposes).
    /// </summary>
    /// <example>ABC-1234</example>
    public string PlateNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current operational status of the vehicle.
    /// </summary>
    /// <example>Active</example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the vehicle is currently available for assignment.
    /// </summary>
    /// <example>true</example>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets the maximum load or passenger capacity of the vehicle.
    /// Measured in kilograms for cargo vehicles, or seats for passenger vehicles.
    /// </summary>
    /// <example>20000</example>
    public int Capacity { get; set; }

    /// <summary>
    /// Gets or sets the full name of the driver currently assigned to the vehicle.
    /// </summary>
    /// <example>John Doe</example>
    public string DriverName { get; set; } = string.Empty;
}

}
