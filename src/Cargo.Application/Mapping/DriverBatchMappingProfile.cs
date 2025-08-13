using AutoMapper;
using Cargo.Application.DTOs.DriverBatches;
using Cargo.Application.DTOs.DriverBatches.Cargo.Application.DTOs;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between DriverBatch entity and its DTO representations.
    /// Handles complex nested collections and value objects for driver pay statements.
    /// </summary>
    public class DriverBatchMappingProfile : Profile
    {
        public DriverBatchMappingProfile()
        {
            // ===== DriverBatch =====
            CreateMap<DriverBatch, DriverBatchDto>();
            CreateMap<DriverBatchCreateDto, DriverBatch>();
            CreateMap<DriverBatchUpdateDto, DriverBatch>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ===== DriverBatchLoad =====
            CreateMap<DriverBatchLoad, DriverBatchLoadDto>();
            CreateMap<DriverBatchLoadCreateDto, DriverBatchLoad>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());




            // ===== DriverBatchWait =====
            CreateMap<DriverBatchWait, DriverBatchWaitDto>();
            CreateMap<DriverBatchWaitCreateDto, DriverBatchWait>()
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            // ===== DriverBatchHourly =====
            CreateMap<DriverBatchHourly, DriverBatchHourlyDto>();
            // ===== DriverBatchHourly =====
            CreateMap<DriverBatchHourlyCreateDto, DriverBatchHourly>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            // ===== Value Objects =====
            CreateMap<TaxProfile, TaxProfileDto>().ReverseMap();
            CreateMap<TaxAmounts, TaxAmountsDto>()
                .ForMember(dest => dest.TotalTaxes,
                           opt => opt.MapFrom(src => src.TotalTaxes))
                .ReverseMap();
        }
    }
}
