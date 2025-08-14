using AutoMapper;
using Cargo.Application.DTOs.VehicleOwnership;
using Cargo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for VehicleOwnership entity and its DTOs.
    /// </summary>
    public class VehicleOwnershipMapingProfile:Profile
    {
        public VehicleOwnershipMapingProfile()
        {
            CreateMap<VehicleOwnershipCreateDto, VehicleOwnership>()
        // Scalars
        .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
        .ForMember(dest => dest.OwnerCompanyId, opt => opt.MapFrom(src => src.OwnerCompanyId))
        .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))          // ← صراحتاً مپ کن
        .ForMember(dest => dest.OwnedFrom, opt => opt.MapFrom(src => src.OwnedFrom))
        .ForMember(dest => dest.OwnedUntil, opt => opt.MapFrom(src => src.OwnedUntil))
        // Ignore infra/nav/audit
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
        .ForMember(dest => dest.OwnerCompany, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
        .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            CreateMap<VehicleOwnershipUpdateDto, VehicleOwnership>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
                .ForMember(dest => dest.OwnerCompanyId, opt => opt.MapFrom(src => src.OwnerCompanyId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))          // ← صراحتاً مپ کن
                .ForMember(dest => dest.OwnedFrom, opt => opt.MapFrom(src => src.OwnedFrom))
                .ForMember(dest => dest.OwnedUntil, opt => opt.MapFrom(src => src.OwnedUntil))
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerCompany, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            // Entity → Read DTO
            CreateMap<VehicleOwnership, VehicleOwnershipDto>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
                .ForMember(dest => dest.OwnerCompany, opt => opt.MapFrom(src => src.OwnerCompany));

            // Brief DTO mappings (optional in read scenarios)
            CreateMap<Vehicle, VehicleBriefDto>()
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber.Value));

            CreateMap<Company, CompanyBriefDto>();

        }
    }
}
