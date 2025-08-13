using AutoMapper;
using Cargo.Application.DTOs.DriverVehicleAssignment;
using Cargo.Application.Mapping;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Tests.Domain
{
    public class DriverVehicleAssignmentProfileTests
    {
        private readonly IMapper _mapper;

        public DriverVehicleAssignmentProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DriverVehicleAssignmentProfile>();
            });



            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_Entity_To_Dto()
        {
            // Arrange
            var entity = new DriverVehicleAssignment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                DriverRoleType.Primary,
                "Test Notes"
            );

            // Act
            var dto = _mapper.Map<DriverVehicleAssignmentDto>(entity);

            // Assert
            Assert.Equal(entity.DriverId, dto.DriverId);
            Assert.Equal(entity.VehicleId, dto.VehicleId);
            Assert.Equal(entity.DriverRole, dto.DriverRole);
            Assert.Equal(entity.Notes, dto.Notes);
        }

        [Fact]
        public void Should_Map_CreateDto_To_Entity()
        {
            // Arrange
            var createDto = new CreateDriverVehicleAssignmentDto
            {
                DriverId = Guid.NewGuid(),
                VehicleId = Guid.NewGuid(),
                DriverRole = DriverRoleType.Primary,
                Notes = "New Assignment"
            };

            // Act
            var entity = _mapper.Map<DriverVehicleAssignment>(createDto);

            // Assert
            Assert.Equal(createDto.DriverId, entity.DriverId);
            Assert.Equal(createDto.VehicleId, entity.VehicleId);
            Assert.Equal(createDto.DriverRole, entity.DriverRole);
            Assert.Equal(createDto.Notes, entity.Notes);
            Assert.Equal(AssignmentStatus.Active, entity.Status); // Constructor default
        }

        [Fact]
        public void Should_Map_UpdateDto_To_Existing_Entity()
        {
            // Arrange - existing entity
            var existingEntity = new DriverVehicleAssignment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                DriverRoleType.Backup,
                "Old Notes"
            );

            var updateDto = new UpdateDriverVehicleAssignmentDto
            {
                Id = existingEntity.Id,
                DriverRole = DriverRoleType.Primary,
                EndedAt = DateTime.UtcNow,
                EndReason = "Completed successfully",
                Notes = "Updated Notes",
                Status = AssignmentStatus.Completed
            };

            // Act - map onto existing entity
            _mapper.Map(updateDto, existingEntity);

            // Assert
            Assert.Equal(DriverRoleType.Primary, existingEntity.DriverRole);
            Assert.Equal(updateDto.EndedAt, existingEntity.EndedAt);
            Assert.Equal(updateDto.EndReason, existingEntity.EndReason);
            Assert.Equal(updateDto.Notes, existingEntity.Notes);
            Assert.Equal(updateDto.Status, existingEntity.Status);
        }
    }
}
