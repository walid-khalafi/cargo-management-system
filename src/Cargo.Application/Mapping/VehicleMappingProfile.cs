using AutoMapper;
using Cargo.Application.DTOs.Vehicle;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for vehicle entity mappings
    /// </summary>
    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            // Vehicle to VehicleDto mapping with enum string conversion
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<VehicleStatus>(src.Status)));
        }
    }
}
