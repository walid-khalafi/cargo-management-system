using AutoMapper;
using Cargo.Application.DTOs.Route;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for route entity mappings
    /// </summary>
    public class RouteMappingProfile : Profile
    {
        public RouteMappingProfile()
        {
            // Route to RouteDto mapping with enum string conversion
            CreateMap<Route, RouteDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RouteType, opt => opt.MapFrom(src => src.RouteType.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<RouteStatus>(src.Status)))
                .ForMember(dest => dest.RouteType, opt => opt.MapFrom(src => Enum.Parse<RouteType>(src.RouteType)));

            // Route to RouteSummaryDto mapping for summary views
            CreateMap<Route, RouteSummaryDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // CreateRouteDto to Route mapping
            CreateMap<CreateRouteDto, Route>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // UpdateRouteDto to Route mapping
            CreateMap<UpdateRouteDto, Route>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}
