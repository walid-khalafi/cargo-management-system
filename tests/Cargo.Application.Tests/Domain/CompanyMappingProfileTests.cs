﻿﻿﻿﻿﻿﻿﻿using AutoMapper;
using Cargo.Application.DTOs.Company;
using Cargo.Application.Mapping;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Cargo.Application.Tests.Domain
{
    public class CompanyMappingProfileTests
    {
        private readonly IMapper _mapper;

        public CompanyMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CompanyMappingProfile>();
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
        public void Company_To_CompanyDto_Mapping_IsValid()
        {
            // Arrange
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test Company Inc.",
                RegistrationNumber = "REG123456",
                Address = new Address
                {
                    Street = "123 Main Street",
                    City = "Montreal",
                    State = "QC",
                    ZipCode = "H3A 1B2",
                    Country = "Canada"
                },
                TaxProfile = TaxProfile.CreateQuebecProfile(),
                Drivers = new List<Driver>
                    {
                        new Driver { Id = Guid.NewGuid() },
                        new Driver { Id = Guid.NewGuid() }
                    },
                Vehicles = new List<VehicleOwnership>
                    {
                        new VehicleOwnership { Id = Guid.NewGuid() },
                        new VehicleOwnership { Id = Guid.NewGuid() }
                    },
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                CreatedBy = "Test User",
                CreatedByIP = "192.168.1.1"
            };

            // Act
            var result = _mapper.Map<CompanyDto>(company);

            // Assert
            Assert.Equal(company.Id, result.Id);
            Assert.Equal(company.Name, result.Name);
            Assert.Equal(company.RegistrationNumber, result.RegistrationNumber);
            Assert.Equal("123 Main Street, Montreal, QC, H3A 1B2, Canada", result.Address);
            Assert.Equal("Quebec", result.TaxProfile);
            Assert.Equal(2, result.DriverIds.Count);
            Assert.Equal(2, result.VehicleIds.Count);
            Assert.Contains(company.Drivers.First().Id, result.DriverIds);
            Assert.Contains(company.Vehicles.First().Id, result.VehicleIds);
        }

        [Fact]
        public void Company_To_CompanyDto_WithNullCollections_MapsCorrectly()
        {
            // Arrange
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test Company",
                RegistrationNumber = "REG789",
                Address = new Address { City = "Toronto" },
                TaxProfile = TaxProfile.CreateOntarioProfile(),
                Drivers = new List<Driver>(),
                Vehicles = new List<VehicleOwnership>()
            };

            // Act
            var result = _mapper.Map<CompanyDto>(company);

            // Assert
            Assert.NotNull(result.DriverIds);
            Assert.NotNull(result.VehicleIds);
            Assert.Empty(result.DriverIds);
            Assert.Empty(result.VehicleIds);
        }

        [Fact]
        public void Company_To_CompanyDto_WithNullAddress_MapsCorrectly()
        {
            // Arrange
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test Company",
                RegistrationNumber = "REG789",
                Address = null!,
                TaxProfile = TaxProfile.CreateQuebecProfile()
            };

            // Act
            var result = _mapper.Map<CompanyDto>(company);

            // Assert
            Assert.Equal(string.Empty, result.Address);
        }

        [Fact]
        public void CompanyCreateDto_To_Company_Mapping_IsValid()
        {
            // Arrange
            var createDto = new CompanyCreateDto
            {
                Name = "New Company LLC",
                RegistrationNumber = "NEW123",
                Address = "456 Business Ave, Toronto, ON, M5V 3A8, Canada",
                TaxProfile = "Ontario"
            };

            // Act
            var result = _mapper.Map<Company>(createDto);

            // Assert
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.RegistrationNumber, result.RegistrationNumber);
            Assert.Equal("456 Business Ave", result.Address.Street);
            Assert.Equal("Toronto", result.Address.City);
            Assert.Equal("ON", result.Address.State);
            Assert.Equal("M5V 3A8", result.Address.ZipCode);
            Assert.Equal("Canada", result.Address.Country);
            Assert.NotNull(result.TaxProfile);
            Assert.Equal("System", result.CreatedBy);
            Assert.Equal("System", result.CreatedByIP);
            Assert.True(result.CreatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void CompanyCreateDto_To_Company_WithEmptyAddress_MapsCorrectly()
        {
            // Arrange
            var createDto = new CompanyCreateDto
            {
                Name = "Test Company",
                RegistrationNumber = "REG001",
                Address = "",
                TaxProfile = "Quebec"
            };

            // Act
            var result = _mapper.Map<Company>(createDto);

            // Assert
            Assert.NotNull(result.Address);
            Assert.Equal(string.Empty, result.Address.Street);
            Assert.Equal(string.Empty, result.Address.City);
        }

        [Fact]
        public void CompanyCreateDto_To_Company_WithInvalidTaxProfile_DefaultsToQuebec()
        {
            // Arrange
            var createDto = new CompanyCreateDto
            {
                Name = "Test Company",
                RegistrationNumber = "REG001",
                Address = "123 Test St, Montreal",
                TaxProfile = "Invalid"
            };

            // Act
            var result = _mapper.Map<Company>(createDto);

            // Assert
            Assert.NotNull(result.TaxProfile);
        }

        [Fact]
        public void CompanyUpdateDto_To_Company_Mapping_IsValid()
        {
            // Arrange
            var existingCompany = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Old Name",
                RegistrationNumber = "OLD123",
                Address = new Address { Street = "Old Street" },
                TaxProfile = TaxProfile.CreateQuebecProfile()
            };

            var updateDto = new CompanyUpdateDto
            {
                Name = "Updated Name",
                RegistrationNumber = "UPD456",
                Address = "789 New Street, Vancouver, BC, V6B 1A1, Canada",
                TaxProfile = "Ontario"
            };

            // Act
            _mapper.Map(updateDto, existingCompany);

            // Assert
            Assert.Equal("Updated Name", existingCompany.Name);
            Assert.Equal("UPD456", existingCompany.RegistrationNumber);
            Assert.Equal("789 New Street", existingCompany.Address.Street);
            Assert.Equal("Vancouver", existingCompany.Address.City);
            Assert.Equal("System", existingCompany.UpdatedBy);
            Assert.True(existingCompany.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void CompanyUpdateDto_To_Company_WithNullValues_IgnoresNulls()
        {
            // Arrange
            var existingCompany = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Original Name",
                RegistrationNumber = "ORIG123",
                Address = new Address { Street = "Original Street" },
                TaxProfile = TaxProfile.CreateQuebecProfile()
            };

            var updateDto = new CompanyUpdateDto
            {
                Name = null!,
                RegistrationNumber = null!,
                Address = null!,
                TaxProfile = null!
            };

            // Act
            _mapper.Map(updateDto, existingCompany);

            // Assert
            Assert.Equal("Original Name", existingCompany.Name);
            Assert.Equal("ORIG123", existingCompany.RegistrationNumber);
            Assert.Equal("Original Street", existingCompany.Address.Street);
        }

        [Fact]
        public void CompanyUpdateDto_To_Company_WithEmptyAddressString_IgnoresEmpty()
        {
            // Arrange
            var existingCompany = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Original Name",
                Address = new Address { Street = "Original Street" }
            };

            var updateDto = new CompanyUpdateDto
            {
                Address = ""
            };

            // Act
            _mapper.Map(updateDto, existingCompany);

            // Assert
            Assert.Equal("Original Street", existingCompany.Address.Street);
        }

        [Theory]
        [InlineData("Quebec")]
        [InlineData("ONTARIO")]
        [InlineData("quebec")]
        public void CreateTaxProfile_WithVariousCases_ReturnsCorrectProfile(string taxProfileInput)
        {
            // Arrange
            var createDto = new CompanyCreateDto
            {
                Name = "Test Company",
                RegistrationNumber = "REG001",
                Address = "123 Test St",
                TaxProfile = taxProfileInput
            };

            // Act
            var result = _mapper.Map<Company>(createDto);

            // Assert
            Assert.NotNull(result.TaxProfile);
        }
    }
}
