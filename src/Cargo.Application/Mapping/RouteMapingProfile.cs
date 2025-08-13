using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.Routes;
using Cargo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Mapping
{
    public class RouteMapingProfile : Profile
    {
        public RouteMapingProfile()
        {


            CreateMap<Route, BaseEntityDto>()
                .ForMember(d => d.CreatedByIP, o => o.Ignore())
                .ForMember(d => d.UpdatedByIP, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore());

            // ===== Read: Entity -> RouteDto =====
            CreateMap<Route, RouteDto>()
                .IncludeBase<Route, BaseEntityDto>()
                .ForMember(d => d.TotalDistanceKm, m => m.MapFrom(s => s.TotalDistance))
                .ForMember(d => d.EstimatedDurationMinutes, m => m.MapFrom(s => (int)Math.Round(s.EstimatedDuration.TotalMinutes)))
                .ForMember(d => d.AssignedVehicleIds, m => m.MapFrom(s => s.AssignedVehicles != null
                        ? s.AssignedVehicles.Select(v => v.Id)
                        : Enumerable.Empty<Guid>()))
                .ForMember(d => d.AssignedDriverIds, m => m.MapFrom(s => s.AssignedDrivers != null
                        ? s.AssignedDrivers.Select(dr => dr.Id)
                        : Enumerable.Empty<Guid>()))
                .ForMember(d => d.IsValid, m => m.MapFrom(s => s.IsValid()))
                .ForMember(d => d.AverageSpeedKph, m => m.MapFrom(s => s.GetAverageSpeed()));


            // ===== Write: CreateDto -> Entity =====
            CreateMap<RouteCreateDto, Route>()
                .ForMember(d => d.Id, o => o.Ignore()) // set in constructor
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.TotalDistance, o => o.MapFrom(s => s.TotalDistanceKm))
                .ForMember(d => d.EstimatedDuration, o => o.MapFrom(s => TimeSpan.FromMinutes(s.EstimatedDurationMinutes)))
                .ForMember(d => d.Status, o => o.MapFrom(_ => Domain.Enums.RouteStatus.Active))
                .ForMember(d => d.AssignedVehicles, o => o.Ignore())
                .ForMember(d => d.AssignedDrivers, o => o.Ignore());


            // ===== Write: UpdateDto -> Entity =====
            CreateMap<RouteUpdateDto, Route>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore()) // AfterMap sets it
                .ForMember(d => d.TotalDistance, o => o.MapFrom(s => s.TotalDistanceKm))
                .ForMember(d => d.EstimatedDuration, o => o.MapFrom(s => TimeSpan.FromMinutes(s.EstimatedDurationMinutes)))
                .ForMember(d => d.AssignedVehicles, o => o.Ignore())
                .ForMember(d => d.AssignedDrivers, o => o.Ignore())
                .AfterMap((src, dest) => dest.UpdatedAt = DateTime.UtcNow);


        }
    }
}
