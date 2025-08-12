using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Domain.ValueObjects;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for common value objects and DTOs
    /// </summary>
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            // Address mappings with ReverseMap for bidirectional mapping
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ZipCode))
                .ReverseMap()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.PostalCode));

            // PlateNumber mappings with ReverseMap and ConstructUsing for proper instantiation
            CreateMap<PlateNumber, PlateNumberDto>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.IssuingAuthority))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.PlateType))
                .ReverseMap()
                .ConstructUsing(src => new PlateNumber(src.Number, src.Province, src.Country));

            // TaxProfile mappings with ReverseMap and ConstructUsing for proper instantiation
            CreateMap<TaxProfile, TaxProfileDto>()
                .ForMember(dest => dest.Province, opt => opt.Ignore())
                .ForMember(dest => dest.FederalTaxRate, opt => opt.Ignore())
                .ForMember(dest => dest.ProvincialTaxRate, opt => opt.Ignore())
                .ForMember(dest => dest.IsQuebecProfile, opt => opt.MapFrom(src => src.QstRate > 0 && src.HstRate == 0))
                .ForMember(dest => dest.IsOntarioProfile, opt => opt.MapFrom(src => src.HstRate > 0))
                .ReverseMap()
                .ConstructUsing(src => new TaxProfile(
                    src.GstRate,
                    src.QstRate,
                    0m, // PST is not used in this context
                    src.HstRate,
                    src.IsQuebecProfile));
        }
    }
}
