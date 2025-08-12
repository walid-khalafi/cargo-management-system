using AutoMapper;
using Cargo.Application.DTOs.Driver;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for driver entity mappings
    /// </summary>
    public class DriverMappingProfile : Profile
    {
        public DriverMappingProfile()
        {
            // Driver to DriverDto mapping with enum string conversion
            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<DriverStatus>(src.Status)));

            // CreateDriverDto to Driver mapping
            CreateMap<CreateDriverDto, Driver>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FullName, opt => opt.Ignore());

            // UpdateDriverDto to Driver mapping
            CreateMap<UpdateDriverDto, Driver>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FullName, opt => opt.Ignore());
        }
    }
}
