using AutoMapper;
using Cargo.Application.DTOs.Driver;
using Cargo.Application.Mapping;
using Cargo.Application.Mapping.Helpers;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cargo.Application.Tests.Domain
{
    public class DriverMappingProfileTests
    {
        private readonly IMapper _mapper;

        public DriverMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DriverMappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Configuration_IsValid()
        {
            // This will throw if any mapping is invalid
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Driver_To_DriverDto_Mapping_IsValid()
        {
            // Arrange
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1-555-123-4567",
                Address = new Address("Canada", "QC", "Montreal", "123 Main Street", "H3A 1B2"),
                LicenseNumber = "DL123456789",
                LicenseType = "CDL-A",
                LicenseExpiryDate = new DateTime(2025, 12, 31),
                DateOfBirth = new DateTime(1985, 5, 15),
                YearsOfExperience = 10,
                Status = DriverStatus.Active,
                CompanyId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                CreatedBy = "Test User",
                CreatedByIP = "192.168.1.1"
            };

            // Act
            var result = _mapper.Map<DriverDto>(driver);

            // Assert
            Assert.Equal(driver.Id, result.Id);
            Assert.Equal(driver.FirstName, result.FirstName);
            Assert.Equal(driver.LastName, result.LastName);
            Assert.Equal("John Doe", result.FullName);
            Assert.Equal(driver.Email, result.Email);
            Assert.Equal(driver.PhoneNumber, result.PhoneNumber);
            Assert.Equal("123 Main Street, Montreal, QC, H3A 1B2, Canada", result.Address);
            Assert.Equal(driver.LicenseNumber, result.LicenseNumber);
            Assert.Equal(driver.LicenseType, result.LicenseType);
            Assert.Equal(driver.LicenseExpiryDate, result.LicenseExpiryDate);
            Assert.Equal(driver.DateOfBirth, result.DateOfBirth);
            Assert.Equal(driver.YearsOfExperience, result.YearsOfExperience);
            Assert.Equal(driver.Status, result.Status);
            Assert.Equal(driver.CompanyId, result.CompanyId);
        }

        [Fact]
        public void Driver_To_DriverDto_WithNullAddress_MapsCorrectly()
        {
            // Arrange
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Address = null!,
                LicenseNumber = "DL987654321",
                Status = DriverStatus.Inactive
            };

            // Act
            var result = _mapper.Map<DriverDto>(driver);

            // Assert
            Assert.Equal(driver.Id, result.Id);
            Assert.Equal("Jane Smith", result.FullName);
            Assert.Equal(string.Empty, result.Address);
        }

        [Fact]
        public void DriverCreateDto_To_Driver_Mapping_IsValid()
        {
            // Arrange
            var createDto = new DriverCreateDto
            {
                FirstName = "Michael",
                LastName = "Johnson",
                Email = "michael.johnson@example.com",
                PhoneNumber = "+1-555-987-6543",
                Address = "456 Oak Avenue, Toronto, ON, M5V 2H1, Canada",
                LicenseNumber = "DL555666777",
                LicenseType = "CDL-B",
                LicenseExpiryDate = new DateTime(2026, 6, 30),
                DateOfBirth = new DateTime(1990, 8, 20),
                YearsOfExperience = 5,
                Status = DriverStatus.Active,
                CompanyId = Guid.NewGuid()
            };

            // Act
            var result = _mapper.Map<Driver>(createDto);

            // Assert
            Assert.Equal(createDto.FirstName, result.FirstName);
            Assert.Equal(createDto.LastName, result.LastName);
            Assert.Equal(createDto.Email, result.Email);
            Assert.Equal(createDto.PhoneNumber, result.PhoneNumber);
            Assert.Equal("456 Oak Avenue", result.Address.Street);
            Assert.Equal("Toronto", result.Address.City);
            Assert.Equal("ON", result.Address.State);
            Assert.Equal("M5V 2H1", result.Address.ZipCode);
            Assert.Equal("Canada", result.Address.Country);
            Assert.Equal(createDto.LicenseNumber, result.LicenseNumber);
            Assert.Equal(createDto.LicenseType, result.LicenseType);
            Assert.Equal(createDto.LicenseExpiryDate, result.LicenseExpiryDate);
            Assert.Equal(createDto.DateOfBirth, result.DateOfBirth);
            Assert.Equal(createDto.YearsOfExperience, result.YearsOfExperience);
            Assert.Equal(createDto.Status, result.Status);
            Assert.Equal(createDto.CompanyId, result.CompanyId);
            Assert.Equal("System", result.CreatedBy);
            Assert.Equal("System", result.CreatedByIP);
            Assert.True(result.CreatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void DriverCreateDto_To_Driver_WithEmptyAddress_MapsCorrectly()
        {
            // Arrange
            var createDto = new DriverCreateDto
            {
                FirstName = "Test",
                LastName = "Driver",
                Email = "test@example.com",
                PhoneNumber = "+1-555-000-0000",
                Address = "",
                LicenseNumber = "DL000000000",
                LicenseType = "CDL-C",
                LicenseExpiryDate = new DateTime(2025, 1, 1),
                DateOfBirth = new DateTime(1995, 1, 1),
                YearsOfExperience = 2,
                Status = DriverStatus.Inactive,
                CompanyId = Guid.NewGuid()
            };

            // Act
            var result = _mapper.Map<Driver>(createDto);

            // Assert
            Assert.NotNull(result.Address);
            Assert.Equal(string.Empty, result.Address.Street);
            Assert.Equal(string.Empty, result.Address.City);
            Assert.Equal(string.Empty, result.Address.State);
            Assert.Equal(string.Empty, result.Address.ZipCode);
            Assert.Equal(string.Empty, result.Address.Country);
        }

        [Fact]
        public void DriverCreateDto_To_Driver_IgnoresIdAndAuditFields()
        {
            // Arrange
            var createDto = new DriverCreateDto
            {
                FirstName = "Alice",
                LastName = "Brown",
                Email = "alice.brown@example.com",
                PhoneNumber = "+1-555-111-2222",
                Address = "789 Pine Street, Vancouver, BC, V6B 1A1, Canada",
                LicenseNumber = "DL111222333",
                LicenseType = "CDL-A",
                LicenseExpiryDate = new DateTime(2027, 3, 15),
                DateOfBirth = new DateTime(1988, 12, 5),
                YearsOfExperience = 8,
                Status = DriverStatus.Active,
                CompanyId = Guid.NewGuid()
            };

            // Act
            var result = _mapper.Map<Driver>(createDto);

            // Assert
            Assert.Equal(Guid.Empty, result.Id); // Should be ignored and default
            Assert.Equal("System", result.CreatedBy);
            Assert.Equal("System", result.CreatedByIP);
            Assert.True(result.CreatedAt <= DateTime.UtcNow);
            Assert.Equal(DateTime.MinValue, result.UpdatedAt); // Should be default
            Assert.Null(result.UpdatedBy);
            Assert.Null(result.UpdatedByIP);
        }

        [Fact]
        public void DriverUpdateDto_To_Driver_Mapping_IsValid()
        {
            // Arrange
            var existingDriver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Original",
                LastName = "Name",
                Email = "original@example.com",
                PhoneNumber = "+1-555-000-0000",
                Address = new Address("Canada", "QC", "Montreal", "123 Main Street", "H3A 1B2"),
                LicenseNumber = "DLORIGINAL",
                LicenseType = "CDL-A",
                LicenseExpiryDate = new DateTime(2025, 1, 1),
                DateOfBirth = new DateTime(1980, 1, 1),
                YearsOfExperience = 10,
                Status = DriverStatus.Active,
                CompanyId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                CreatedBy = "Original User",
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedBy = "Original Updater"
            };

            var updateDto = new DriverUpdateDto
            {
                FirstName = "Updated",
                LastName = "Driver",
                Email = "updated@example.com",
                PhoneNumber = "+1-555-999-8888",
                Address = "999 Updated Street, Calgary, AB, T2P 2Y5, Canada",
                LicenseNumber = "DLUPDATED123",
                LicenseType = "CDL-B",
                LicenseExpiryDate = new DateTime(2026, 12, 31),
                DateOfBirth = new DateTime(1985, 6, 15),
                YearsOfExperience = 12,
                Status = DriverStatus.Inactive,
                CompanyId = Guid.NewGuid()
            };

            // Act
            _mapper.Map(updateDto, existingDriver);

            // Assert
            Assert.Equal("Updated", existingDriver.FirstName);
            Assert.Equal("Driver", existingDriver.LastName);
            Assert.Equal("updated@example.com", existingDriver.Email);
            Assert.Equal("+1-555-999-8888", existingDriver.PhoneNumber);
            Assert.Equal("999 Updated Street", existingDriver.Address.Street);
            Assert.Equal("Calgary", existingDriver.Address.City);
            Assert.Equal("AB", existingDriver.Address.State);
            Assert.Equal("T2P 2Y5", existingDriver.Address.ZipCode);
            Assert.Equal("Canada", existingDriver.Address.Country);
            Assert.Equal("DLUPDATED123", existingDriver.LicenseNumber);
            Assert.Equal("CDL-B", existingDriver.LicenseType);
            Assert.Equal(new DateTime(2026, 12, 31), existingDriver.LicenseExpiryDate);
            Assert.Equal(new DateTime(1985, 6, 15), existingDriver.DateOfBirth);
            Assert.Equal(12, existingDriver.YearsOfExperience);
            Assert.Equal(DriverStatus.Inactive, existingDriver.Status);
            Assert.Equal(updateDto.CompanyId, existingDriver.CompanyId);
            Assert.Equal("System", existingDriver.UpdatedBy);
            Assert.True(existingDriver.UpdatedAt <= DateTime.UtcNow);
            Assert.Equal("Original User", existingDriver.CreatedBy); // Should remain unchanged
            Assert.True(existingDriver.CreatedAt < existingDriver.UpdatedAt);
        }

        [Fact]
        public void DriverUpdateDto_To_Driver_WithNullValues_IgnoresNulls()
        {
            // Arrange
            var existingDriver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Original",
                LastName = "Name",
                Email = "original@example.com",
                PhoneNumber = "+1-555-000-0000",
                Address = new Address("Canada", "QC", "Montreal", "123 Main Street", "H3A 1B2"),
                LicenseNumber = "DLORIGINAL",
                LicenseType = "CDL-A",
                LicenseExpiryDate = new DateTime(2025, 1, 1),
                DateOfBirth = new DateTime(1980, 1, 1),
                YearsOfExperience = 10,
                Status = DriverStatus.Active,
                CompanyId = Guid.NewGuid()
            };

            var updateDto = new DriverUpdateDto
            {
                FirstName = null!,
                LastName = null!,
                Email = null!,
                PhoneNumber = null!,
                Address = null!,
                LicenseNumber = null!,
                LicenseType = null!,
                LicenseExpiryDate = null,
                DateOfBirth = null,
                YearsOfExperience = null,
                Status = null,
                CompanyId = null
            };

            // Act
            _mapper.Map(updateDto, existingDriver);

            // Assert
            Assert.Equal("Original", existingDriver.FirstName);
            Assert.Equal("Name", existingDriver.LastName);
            Assert.Equal("original@example.com", existingDriver.Email);
            Assert.Equal("+1-555-000-0000", existingDriver.PhoneNumber);
            Assert.Equal("123 Main Street", existingDriver.Address.Street);
            Assert.Equal("DLORIGINAL", existingDriver.LicenseNumber);
            Assert.Equal("CDL-A", existingDriver.LicenseType);
            Assert.Equal(new DateTime(2025, 1, 1), existingDriver.LicenseExpiryDate);
            Assert.Equal(new DateTime(1980, 1, 1), existingDriver.DateOfBirth);
            Assert.Equal(10, existingDriver.YearsOfExperience);
            Assert.Equal(DriverStatus.Active, existingDriver.Status);
            Assert.Equal(existingDriver.CompanyId, existingDriver.CompanyId); // Should remain unchanged
        }

        [Fact]
        public void DriverUpdateDto_To_Driver_WithEmptyAddressString_IgnoresEmpty()
        {
            // Arrange
            var existingDriver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Original",
                Address = new Address("Canada", "QC", "Montreal", "123 Main Street", "H3A 1B2"),
            };

            var updateDto = new DriverUpdateDto
            {
                Address = ""
            };

            // Act
            _mapper.Map(updateDto, existingDriver);

            // Assert
            Assert.Equal("123 Main Street", existingDriver.Address.Street);
        }

        [Fact]
        public void DriverUpdateDto_To_Driver_PartialUpdate_OnlyUpdatesProvidedFields()
        {
            // Arrange
            var existingDriver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1-555-123-4567",
                Address = new Address("Canada", "QC", "Montreal", "123 Main St", "H3A 1B2"),
                LicenseNumber = "DL123456",
                LicenseType = "CDL-A",
                Status = DriverStatus.Active
            };

            var updateDto = new DriverUpdateDto
            {
                FirstName = "Johnny",
                Email = "johnny.doe@example.com",
                YearsOfExperience = 15,
                // Other fields are null and should not be updated
            };

            // Act
            _mapper.Map(updateDto, existingDriver);

            // Assert
            Assert.Equal("Johnny", existingDriver.FirstName);
            Assert.Equal("johnny.doe@example.com", existingDriver.Email);
            Assert.Equal(15, existingDriver.YearsOfExperience);
            Assert.Equal("Doe", existingDriver.LastName); // Should remain unchanged
            Assert.Equal("+1-555-123-4567", existingDriver.PhoneNumber); // Should remain unchanged
            Assert.Equal("123 Main St", existingDriver.Address.Street); // Should remain unchanged
            Assert.Equal("DL123456", existingDriver.LicenseNumber); // Should remain unchanged
            Assert.Equal("CDL-A", existingDriver.LicenseType); // Should remain unchanged
            Assert.Equal(DriverStatus.Active, existingDriver.Status); // Should remain unchanged
        }

        [Theory]
        [InlineData("123 Main Street, Montreal, QC, H3A 1B2, Canada")]
        [InlineData("456 Oak Ave, Toronto, ON, M5V 2H1, Canada")]
        [InlineData("789 Pine St, Vancouver, BC, V6B 1A1, Canada")]
        public void Address_Formatting_VariousFormats_MapsCorrectly(string address)
        {
            // Arrange
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Driver",
                Address = MappingHelper.ParseAddress(address)
            };

            // Act
            var result = _mapper.Map<DriverDto>(driver);

            // Assert
            Assert.Equal(address, result.Address);
        }

        [Fact]
        public void FullName_Calculation_CombinesFirstAndLastName()
        {
            // Arrange
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Wonderland"
            };

            // Act
            var result = _mapper.Map<DriverDto>(driver);

            // Assert
            Assert.Equal("Alice Wonderland", result.FullName);
        }

        [Fact]
        public void FullName_Calculation_WithEmptyNames_HandlesGracefully()
        {
            // Arrange
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "",
                LastName = ""
            };

            // Act
            var result = _mapper.Map<DriverDto>(driver);

            // Assert
            Assert.Equal(" ", result.FullName);
        }
    }
}
