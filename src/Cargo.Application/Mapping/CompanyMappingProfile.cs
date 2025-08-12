using AutoMapper;
using Cargo.Application.DTOs.Company;
using Cargo.Domain.Entities;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for company entity mappings
    /// </summary>
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            // Company to CompanyDto mapping with ReverseMap
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.DriverCount, opt => opt.MapFrom(src => src.Drivers.Count))
                .ForMember(dest => dest.VehicleCount, opt => opt.MapFrom(src => src.Vehicles.Count))
                .ReverseMap()
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore());

            // Company to CompanySummaryDto mapping for summary views
            CreateMap<Company, CompanySummaryDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.DriverCount, opt => opt.MapFrom(src => src.Drivers.Count))
                .ForMember(dest => dest.VehicleCount, opt => opt.MapFrom(src => src.Vehicles.Count));

            // CreateCompanyDto to Company mapping
            CreateMap<CreateCompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore());

            // UpdateCompanyDto to Company mapping
            CreateMap<UpdateCompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore());
        }
    }
}
