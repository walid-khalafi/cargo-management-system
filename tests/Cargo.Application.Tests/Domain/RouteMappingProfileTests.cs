using AutoMapper;
using Cargo.Application.DTOs.Routes;
using Cargo.Application.Mapping;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cargo.Application.Tests.Domain
{
    public class RouteMappingProfileTests
    {
        private readonly IMapper _mapper;

        public RouteMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RouteMapingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        #region Entity to DTO Mapping Tests

        [Fact]
        public void Map_Route_To_RouteDto_ShouldMapAllProperties()
        {
            // Arrange
            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = "Test Route",
                Description = "Test Description",
                Origin = "New York",
                Destination = "Los Angeles",
                Waypoints = new List<string> { "Chicago", "Denver" },
                TotalDistance = 2789.5,
                EstimatedDuration = TimeSpan.FromHours(41.5),
                EstimatedFuelCost = 450.75m,
                EstimatedTollCost = 125.50m,
                RouteType = RouteType.Highway,
                Status = RouteStatus.Active,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            };

            // Act
            var result = _mapper.Map<RouteDto>(route);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(route.Id, result.Id);
            Assert.Equal(route.Name, result.Name);
            Assert.Equal(route.Description, result.Description);
            Assert.Equal(route.Origin, result.Origin);
            Assert.Equal(route.Destination, result.Destination);
            Assert.Equal(route.Waypoints, result.Waypoints);
            Assert.Equal(route.TotalDistance, result.TotalDistanceKm);
            Assert.Equal(2490, result.EstimatedDurationMinutes);
            Assert.Equal(route.EstimatedFuelCost, result.EstimatedFuelCost);
            Assert.Equal(route.EstimatedTollCost, result.EstimatedTollCost);
            Assert.Equal(route.RouteType, result.RouteType);
            Assert.Equal(route.Status, result.Status);
            Assert.Equal(route.CreatedAt, result.CreatedAt);
            Assert.Equal(route.UpdatedAt, result.UpdatedAt);
            Assert.True(result.IsValid);
            Assert.Equal(67.2, Math.Round(result.AverageSpeedKph, 1));
        }

        [Fact]
        public void Map_Route_To_RouteDto_ShouldHandleNullCollections()
        {
            // Arrange
            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = "Test Route",
                AssignedVehicles = null,
                AssignedDrivers = null
            };

            // Act
            var result = _mapper.Map<RouteDto>(route);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AssignedVehicleIds);
            Assert.Empty(result.AssignedVehicleIds);
            Assert.NotNull(result.AssignedDriverIds);
            Assert.Empty(result.AssignedDriverIds);
        }

        [Fact]
        public void Map_Route_To_RouteDto_ShouldHandleEmptyCollections()
        {
            // Arrange
            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = "Test Route",
                AssignedVehicles = new List<Vehicle>(),
                AssignedDrivers = new List<Driver>()
            };

            // Act
            var result = _mapper.Map<RouteDto>(route);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AssignedVehicleIds);
            Assert.Empty(result.AssignedVehicleIds);
            Assert.NotNull(result.AssignedDriverIds);
            Assert.Empty(result.AssignedDriverIds);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(100, 0, 0)]
        [InlineData(0, 2, 0)]
        public void Map_Route_To_RouteDto_ShouldCalculateAverageSpeedCorrectly(double distance, double durationHours, double expectedSpeed)
        {
            // Arrange
            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = "Test Route",
                TotalDistance = distance,
                EstimatedDuration = TimeSpan.FromHours(durationHours)
            };

            // Act
            var result = _mapper.Map<RouteDto>(route);

            // Assert
            Assert.Equal(expectedSpeed, result.AverageSpeedKph);
        }

        #endregion

        #region CreateDto to Entity Mapping Tests

        [Fact]
        public void Map_RouteCreateDto_To_Route_ShouldMapAllProperties()
        {
            // Arrange
            var createDto = new RouteCreateDto
            {
                Name = "New Route",
                Description = "New Route Description",
                Origin = "Seattle",
                Destination = "Portland",
                Waypoints = new List<string> { "Tacoma", "Olympia" },
                TotalDistanceKm = 280.5,
                EstimatedDurationMinutes = 180,
                EstimatedFuelCost = 45.25m,
                EstimatedTollCost = 12.75m,
                RouteType = RouteType.Local,
                AssignedVehicleIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                AssignedDriverIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            // Act
            var result = _mapper.Map<Route>(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Description, result.Description);
            Assert.Equal(createDto.Origin, result.Origin);
            Assert.Equal(createDto.Destination, result.Destination);
            Assert.Equal(createDto.Waypoints, result.Waypoints);
            Assert.Equal(createDto.TotalDistanceKm, result.TotalDistance);
            Assert.Equal(TimeSpan.FromMinutes(createDto.EstimatedDurationMinutes), result.EstimatedDuration);
            Assert.Equal(createDto.EstimatedFuelCost, result.EstimatedFuelCost);
            Assert.Equal(createDto.EstimatedTollCost, result.EstimatedTollCost);
            Assert.Equal(createDto.RouteType, result.RouteType);
            Assert.Equal(RouteStatus.Active, result.Status);
            Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 5);
            Assert.Null(result.UpdatedAt);
        }

        [Fact]
        public void Map_RouteCreateDto_To_Route_ShouldIgnoreIdAndCreatedAt()
        {
            // Arrange
            var createDto = new RouteCreateDto
            {
                Name = "Test Route"
            };

            // Act
            var result = _mapper.Map<Route>(createDto);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 5);
        }

        [Fact]
        public void Map_RouteCreateDto_To_Route_ShouldHandleNullWaypoints()
        {
            // Arrange
            var createDto = new RouteCreateDto
            {
                Name = "Test Route",
                Waypoints = null
            };

            // Act
            var result = _mapper.Map<Route>(createDto);

            // Assert
            Assert.NotNull(result.Waypoints);
            Assert.Empty(result.Waypoints);
        }

        #endregion

        #region UpdateDto to Entity Mapping Tests

        [Fact]
        public void Map_RouteUpdateDto_To_Route_ShouldMapAllProperties()
        {
            // Arrange
            var updateDto = new RouteUpdateDto
            {
                Name = "Updated Route",
                Description = "Updated Description",
                Origin = "Boston",
                Destination = "Miami",
                Waypoints = new List<string> { "New York", "Washington DC" },
                TotalDistanceKm = 2050.75,
                EstimatedDurationMinutes = 2880,
                EstimatedFuelCost = 320.50m,
                EstimatedTollCost = 85.25m,
                RouteType = RouteType.Highway,
                Status = RouteStatus.Inactive,
                AssignedVehicleIds = new List<Guid> { Guid.NewGuid() },
                AssignedDriverIds = new List<Guid> { Guid.NewGuid() }
            };

            var originalRoute = new Route
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            // Act
            var result = _mapper.Map(updateDto, originalRoute);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(originalRoute.Id, result.Id);
            Assert.Equal(originalRoute.CreatedAt, result.CreatedAt);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Description, result.Description);
            Assert.Equal(updateDto.Origin, result.Origin);
            Assert.Equal(updateDto.Destination, result.Destination);
            Assert.Equal(updateDto.Waypoints, result.Waypoints);
            Assert.Equal(updateDto.TotalDistanceKm, result.TotalDistance);
            Assert.Equal(TimeSpan.FromMinutes(updateDto.EstimatedDurationMinutes), result.EstimatedDuration);
            Assert.Equal(updateDto.EstimatedFuelCost, result.EstimatedFuelCost);
            Assert.Equal(updateDto.EstimatedTollCost, result.EstimatedTollCost);
            Assert.Equal(updateDto.RouteType, result.RouteType);
            Assert.Equal(updateDto.Status, result.Status);
            Assert.True((DateTime.UtcNow - result.UpdatedAt.Value).TotalSeconds < 5);
        }

        [Fact]
        public void Map_RouteUpdateDto_To_Route_ShouldPreserveIdAndCreatedAt()
        {
            // Arrange
            var updateDto = new RouteUpdateDto
            {
                Name = "Updated Route"
            };

            var originalRoute = new Route
            {
                Id = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            };

            // Act
            var result = _mapper.Map(updateDto, originalRoute);

            // Assert
            Assert.Equal(originalRoute.Id, result.Id);
            Assert.Equal(originalRoute.CreatedAt, result.CreatedAt);
        }

        [Fact]
        public void Map_RouteUpdateDto_To_Route_ShouldUpdateUpdatedAt()
        {
            // Arrange
            var updateDto = new RouteUpdateDto
            {
                Name = "Updated Route"
            };

            var originalRoute = new Route
            {
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            };

            // Act
            var result = _mapper.Map(updateDto, originalRoute);

            // Assert
            Assert.True((DateTime.UtcNow - result.UpdatedAt.Value).TotalSeconds < 5);
        }

        #endregion

        #region Configuration Validation

        [Fact]
        public void Configuration_ShouldBeValid()
        {
            // Arrange & Act
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RouteMapingProfile>();
            });

            // Assert
            configuration.AssertConfigurationIsValid();
        }

        #endregion
    }
}
