using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.Vehicles;
using Cargo.Application.Mapping;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using System;
using Xunit;

namespace Cargo.Application.Tests.Domain
{
    public class VehicleProfileTests
    {
        private readonly IMapper _mapper;

        public VehicleProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VehicleProfile>();
            });

            // Throws if any mapping is invalid
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Configuration_is_valid()
        {
            // Already validated in constructor; this is a sanity check
            Assert.NotNull(_mapper);
        }

        [Fact]
        public void Should_map_PlateNumberDto_to_PlateNumber_on_create()
        {
            // Arrange
            var createDto = new VehicleCreateDto
            {
                PlateNumber = new PlateNumberDto
                {
                    Value = "IR-12-A-345",
                    IssuingAuthority = "IR",
                    PlateType = "Commercial"
                },
                Make = "Ford",
                Model = "F-150",
                Year = 2023,
                Color = "Blue",
                VIN = "1FTFW1E14PFA12345",
                RegistrationNumber = "REG123",
                FuelType = "Diesel",
                Capacity = 5
            };

            // Act
            var entity = _mapper.Map<Vehicle>(createDto);

            // Assert
            Assert.NotNull(entity.PlateNumber);
            Assert.Equal("IR-12-A-345", entity.PlateNumber.Value);
            Assert.Equal("IR", entity.PlateNumber.IssuingAuthority);
            Assert.Equal("Commercial", entity.PlateNumber.PlateType);
        }
        [Fact]
        public void Should_throw_when_PlateNumberDto_is_null_on_create_due_to_domain_invariant()
        {
            // Arrange
            var createDto = new VehicleCreateDto
            {
                PlateNumber = null,
                Make = "Ford",
                Model = "F-150",
                Year = 2023,
                Color = "Blue",
                VIN = "1FTFW1E14PFA12345",
                RegistrationNumber = "REG123",
                FuelType = "Diesel",
                Capacity = 5,
                OwnerCompanyId = Guid.NewGuid()
            };

            // Act
            var ex = Record.Exception(() => _mapper.Map<Vehicle>(createDto));

            // Assert
            Assert.NotNull(ex);
            if (ex is AutoMapper.AutoMapperMappingException amEx && amEx.InnerException != null)
                Assert.IsType<ArgumentNullException>(amEx.InnerException);
            else
                Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Should_map_Vehicle_to_VehicleDto_with_nested_plate_number()
        {
            // Arrange
            var entity = new Vehicle(
                "Ford",
                "F-150",
                2023,
                "Blue",
                "1FTFW1E14PFA12345",
                "REG123",
                new PlateNumber("QTR-12345", "QA", "Standard"),
                "Diesel",
                5,
                Guid.NewGuid()
            );

            // Act
            var dto = _mapper.Map<VehicleDto>(entity);

            // Assert
            Assert.NotNull(dto.PlateNumber);
            Assert.Equal("QTR-12345", dto.PlateNumber.Value);
            Assert.Equal("QA", dto.PlateNumber.IssuingAuthority);
            Assert.Equal("Standard", dto.PlateNumber.PlateType);
        }

        [Fact]
        public void Update_should_map_plate_number_and_touch_UpdatedAt()
        {
            // Arrange
            var ownerCompanyId = Guid.NewGuid();
            var existing = new Vehicle(
                "Ford",
                "F-150",
                2023,
                "Red",
                "1FTFW1E14PFA12345",
                "REG123",
                new PlateNumber("OLD-999", "IR", "Standard"),
                "Diesel",
                5,
                ownerCompanyId
            );
            existing.UpdatedAt = DateTime.UtcNow.AddDays(-1);

            var updateDto = new VehicleUpdateDto
            {
                PlateNumber = new PlateNumberDto
                {
                    Value = "NEW-777",
                    IssuingAuthority = "QA",
                    PlateType = "Commercial"
                },
                Make = "Chevrolet",
                Model = "Silverado",
                Year = 2024,
                Color = "Red",
                VIN = "1GC4KYEY9PF123456",
                RegistrationNumber = "REG456",
                FuelType = "Diesel",
                Capacity = 6
            };

            // Act
            _mapper.Map(updateDto, existing);

            // Assert
            Assert.NotNull(existing.PlateNumber);
            Assert.Equal("NEW-777", existing.PlateNumber.Value);
            Assert.Equal("QA", existing.PlateNumber.IssuingAuthority);
            Assert.Equal("Commercial", existing.PlateNumber.PlateType);

            // UpdatedAt should be set near now by AfterMap
            Assert.True((DateTime.UtcNow - existing.UpdatedAt).Value.TotalSeconds < 3);
        }

        [Fact]
        public void Update_with_null_plate_should_set_plate_to_null_and_touch_UpdatedAt()
        {
            // Arrange
            var ownerCompanyId = Guid.NewGuid();
            var existing = new Vehicle(
                "Ford",
                "F-150",
                2023,
                "Red",
                "1FTFW1E14PFA12345",
                "REG123",
                new PlateNumber("KEEP-111", "IR", "Standard"),
                "Diesel",
                5,
                ownerCompanyId
            );
            existing.UpdatedAt = DateTime.UtcNow.AddDays(-1);

            var updateDto = new VehicleUpdateDto
            {
                PlateNumber = null,
                Make = "Chevrolet",
                Model = "Silverado",
                Year = 2024,
                Color = "Red",
                VIN = "1GC4KYEY9PF123456",
                RegistrationNumber = "REG456",
                FuelType = "Diesel",
                Capacity = 6
            };

            // Act
            _mapper.Map(updateDto, existing);

            // Assert
            Assert.Null(existing.PlateNumber);
            Assert.True((DateTime.UtcNow - existing.UpdatedAt).Value.TotalSeconds < 3);
        }
    }
}
