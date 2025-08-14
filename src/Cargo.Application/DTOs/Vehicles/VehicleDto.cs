using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Vehicles
{
    /// <summary>
    /// Full vehicle details for read operations.
    /// </summary>
    public class VehicleDto : BaseEntityDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public string RegistrationNumber { get; set; }
        public PlateNumberDto PlateNumber { get; set; } 
        public string FuelType { get; set; }
        public int Capacity { get; set; }
        public VehicleStatus Status { get; set; }
        public bool IsAvailable { get; set; }
        public int Mileage { get; set; }
        public string CurrentLocation { get; set; }
        public Guid? DriverId { get; set; }
        public Guid OwnerCompanyId { get; set; }
    }

}
