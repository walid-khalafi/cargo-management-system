using AutoMapper;
using Cargo.Domain.Enums;
using global::Cargo.Application.DTOs.DriverVehicleAssignment;
using global::Cargo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Mapping
{

    /// <summary>
    /// AutoMapper profile for mapping between DriverVehicleAssignment entity
    /// and its related Data Transfer Objects (DTOs) for read, create, and update operations.
    /// </summary>
    public class DriverVehicleAssignmentProfile : Profile
    {
        public DriverVehicleAssignmentProfile()
        {
            // Map from Entity → Read DTO
            // Used when returning assignment data to clients.
            CreateMap<DriverVehicleAssignment, DriverVehicleAssignmentDto>();

            // Map from Create DTO → Entity
            // Uses the domain constructor to ensure business rules & validations are applied.
            CreateMap<CreateDriverVehicleAssignmentDto, DriverVehicleAssignment>()
            // Map the obvious ones
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Will be generated in ctor or DB
            .ForMember(dest => dest.Driver, opt => opt.Ignore())
            .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.EndedAt, opt => opt.Ignore())
            .ForMember(dest => dest.EndReason, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => AssignmentStatus.Active))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
             .AfterMap((src, dest) =>
             {
                 dest.CreatedAt = DateTime.UtcNow;
                 dest.CreatedBy = "System";
                 dest.CreatedByIP = "System";
             });

            // Map from Update DTO → Entity
            // Ignores properties that shouldn't be overwritten directly by updates
            // (IDs, navigation properties, and fields controlled by business logic).
            CreateMap<UpdateDriverVehicleAssignmentDto, DriverVehicleAssignment>()
     .ForMember(dest => dest.AssignedAt, opt => opt.Ignore())
     .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
     .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
     .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
     .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
     .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
     .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
      .AfterMap((src, dest) =>
      {
          dest.UpdatedAt = DateTime.UtcNow;
          dest.UpdatedBy = "System";
          dest.UpdatedByIP = "System";
      });
        }
    }
}
