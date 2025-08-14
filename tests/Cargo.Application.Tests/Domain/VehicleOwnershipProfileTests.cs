using AutoMapper;
using Cargo.Application.DTOs.VehicleOwnership;
using Cargo.Application.Mapping;
using Cargo.Application.Mapping.Helpers;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Tests.Domain
{
    public class VehicleOwnershipProfileTests
    {
        private readonly IMapper _mapper;

        public VehicleOwnershipProfileTests()
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.AddProfile<VehicleOwnershipMapingProfile>();
            });

            cfg.AssertConfigurationIsValid();
            _mapper = cfg.CreateMapper();
        }

        [Fact]
        public void Configuration_should_be_valid()
        {
            // Already validated in ctor; this is an explicit assertion for test reports.
            (_mapper.ConfigurationProvider as MapperConfiguration)!.AssertConfigurationIsValid();
        }


        [Fact]
        public void Map_create_dto_to_entity_should_map_scalars_and_ignore_navs()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var dto = new VehicleOwnershipCreateDto
            {
                VehicleId = vehicleId,
                OwnerCompanyId = companyId,
                Type = OwnershipType.OwnedByDriverCompany,
                OwnedFrom = new DateTime(2024, 01, 15, 0, 0, 0, DateTimeKind.Utc),
                OwnedUntil = null
            };

            // Act
            var entity = _mapper.Map<VehicleOwnership>(dto);

            // Assert
            Assert.Equal(vehicleId, entity.VehicleId);
            Assert.Equal(companyId, entity.OwnerCompanyId);
            Assert.Equal(OwnershipType.OwnedByDriverCompany, entity.Type);
            Assert.Equal(new DateTime(2024, 01, 15, 0, 0, 0, DateTimeKind.Utc), entity.OwnedFrom);
            Assert.Null(entity.OwnedUntil);


            // Navigations are ignored on create
            Assert.Null(entity.Vehicle);
            Assert.Null(entity.OwnerCompany);
        }


        [Fact]
        public void Map_update_dto_onto_existing_entity_should_update_scalars_and_preserve_navs()
        {
            // Arrange
            var originalVehicle = MappingHelper.BuildVehicle(Guid.NewGuid(), "Ford", "F-150", "ABC-123");
            var originalCompany = MappingHelper.BuildCompany(Guid.NewGuid(), "Original Co");

            var entity = new VehicleOwnership
            {
                Id = Guid.NewGuid(),
                VehicleId = originalVehicle.Id,
                OwnerCompanyId = originalCompany.Id,
                Type = OwnershipType.OwnedByFleet,
                OwnedFrom = new DateTime(2023, 06, 01, 0, 0, 0, DateTimeKind.Utc),
                OwnedUntil = null,
                Vehicle = originalVehicle,
                OwnerCompany = originalCompany
            };

            var newVehicleId = Guid.NewGuid();
            var newCompanyId = Guid.NewGuid();
            var dto = new VehicleOwnershipUpdateDto
            {
                Id = entity.Id,
                VehicleId = newVehicleId,
                OwnerCompanyId = newCompanyId,
                Type = OwnershipType.OwnedByFleet,
                OwnedFrom = new DateTime(2024, 02, 01, 0, 0, 0, DateTimeKind.Utc),
                OwnedUntil = new DateTime(2025, 02, 01, 0, 0, 0, DateTimeKind.Utc)
            };

            // Act
            _mapper.Map(dto, entity);

            // Assert: scalars updated
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(newVehicleId, entity.VehicleId);
            Assert.Equal(newCompanyId, entity.OwnerCompanyId);
            Assert.Equal(OwnershipType.OwnedByFleet, entity.Type);
            Assert.Equal(new DateTime(2024, 02, 01, 0, 0, 0, DateTimeKind.Utc), entity.OwnedFrom);
            Assert.Equal(new DateTime(2025, 02, 01, 0, 0, 0, DateTimeKind.Utc), entity.OwnedUntil);

            // Assert: navigations preserved (ignored by mapping)
            Assert.Same(originalVehicle, entity.Vehicle);
            Assert.Same(originalCompany, entity.OwnerCompany);
        }

        [Fact]
        public void Map_entity_to_read_dto_should_include_briefs_and_plate_number_value()
        {
            // Arrange
            var vehicle =MappingHelper.BuildVehicle(Guid.NewGuid(), "Volvo", "FH16", "QAT-5678");
            var company = MappingHelper.BuildCompany(Guid.NewGuid(), "Qatar Logistics");

            var entity = new VehicleOwnership
            {
                Id = Guid.NewGuid(),
                VehicleId = vehicle.Id,
                OwnerCompanyId = company.Id,
                Type = OwnershipType.OwnedByFleet,
                OwnedFrom = new DateTime(2022, 09, 10, 0, 0, 0, DateTimeKind.Utc),
                OwnedUntil = null,
                Vehicle = vehicle,
                OwnerCompany = company
            };

            // Act

            var dto = _mapper.Map<VehicleOwnershipDto>(entity);

            // Assert: top-level
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.VehicleId, dto.VehicleId);
            Assert.Equal(entity.OwnerCompanyId, dto.OwnerCompanyId);
            Assert.Equal(entity.Type, dto.Type);
            Assert.Equal(entity.OwnedFrom, dto.OwnedFrom);
            Assert.Null(dto.OwnedUntil);

            // Assert: brief projections
            Assert.NotNull(dto.Vehicle);
            Assert.Equal(vehicle.Id, dto.Vehicle!.Id);
            Assert.Equal("Volvo", dto.Vehicle.Make);
            Assert.Equal("FH16", dto.Vehicle.Model);
            Assert.Equal("QAT-5678", dto.Vehicle.PlateNumber); // from value object .Value

            Assert.NotNull(dto.OwnerCompany);
            Assert.Equal(company.Id, dto.OwnerCompany!.Id);
            Assert.Equal("Qatar Logistics", dto.OwnerCompany.Name);
        }





    }
}
