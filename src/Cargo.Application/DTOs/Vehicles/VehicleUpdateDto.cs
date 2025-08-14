using Cargo.Application.DTOs.Common;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.Vehicles
{
    public class VehicleUpdateDto
    {
        [Required, MaxLength(100)]
        public string Make { get; set; }

        [Required, MaxLength(100)]
        public string Model { get; set; }

        [Range(1980, 2100)]
        public int Year { get; set; }

        public string Color { get; set; }

        [Required, MaxLength(50)]
        public string VIN { get; set; }

        public string RegistrationNumber { get; set; }

        [Required]
        public PlateNumberDto PlateNumber { get; set; }

        public string FuelType { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public string CurrentLocation { get; set; }
        public Guid? DriverId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
