using AutoMapper;
using Cargo.Application.DTOs.DriverBatch;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for driver batch entity mappings
    /// </summary>
    public class DriverBatchMappingProfile : Profile
    {
        public DriverBatchMappingProfile()
        {
            // DriverBatch to DriverBatchDto mapping with enum string conversion
            CreateMap<DriverBatch, DriverBatchDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BatchStatus>(src.Status)));

            // DriverBatch to DriverBatchSummaryDto mapping for summary views
            CreateMap<DriverBatch, DriverBatchSummaryDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // CreateDriverBatchDto to DriverBatch mapping
            CreateMap<CreateDriverBatchDto, DriverBatch>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // DriverBatch entries mappings
            CreateMap<DriverBatchHourly, DriverBatchHourlyDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<DriverBatchLoad, DriverBatchLoadDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<DriverBatchWait, DriverBatchWaitDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
