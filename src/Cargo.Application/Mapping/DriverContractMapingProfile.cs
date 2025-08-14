using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.DriverContracts;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Cargo.Application.MappingProfiles
{
    // Maps DriverContract aggregate and its nested value objects/DTOs.
    // Important: Use ConstructUsing for immutable value objects (parameterized constructors),
    // and ignore read-only collections to avoid mutation attempts by AutoMapper.
    public class DriverContractMapingProfile : Profile
    {
        public DriverContractMapingProfile()
        {
            // --------------------------------
            // Primitive/value object maps used by nested mappings
            // --------------------------------
            // TaxProfile ↔ TaxProfileDto
            // Note: Value object requires explicit construction.
            CreateMap<TaxProfile, TaxProfileDto>();
            CreateMap<TaxProfileDto, TaxProfile>()
                .ConstructUsing(src => new TaxProfile(
                    src.GstRate,
                    src.QstRate,
                    src.PstRate,
                    src.HstRate,
                    src.CompoundQstOverGst
                ));

            // DriverSettings ↔ DriverSettingsDto (includes nested TaxProfile)
            // Entity → DTO can be default (DTO is mutable); we still specify path for clarity.
            CreateMap<DriverSettings, DriverSettingsDto>()
                .ForMember(d => d.TaxProfile, opt => opt.MapFrom(s => s.TaxProfile));
            CreateMap<DriverSettingsDto, DriverSettings>()
                .ConstructUsing((src, ctx) => new DriverSettings(
                    src.NumPayBands,
                    src.HourlyRate,
                    src.FscRate,
                    src.FscMode,
                    src.WaitingPerMinute,
                    src.AdminFee,
                    src.Province,
                    ctx.Mapper.Map<TaxProfile>(src.TaxProfile)
                ));

            // RateBand ↔ RateBandDto
            // Assumes RateBand has parameterless ctor and settable properties.
            CreateMap<RateBand, RateBandDto>().ReverseMap();

            // --------------------------------
            // Entity → DTO
            // --------------------------------
            CreateMap<DriverContract, DriverContractDto>()
        .ForMember(
            d => d.DriverName,
            opt => opt.MapFrom(s => s.Driver.FullName)
        )
        .ForMember(
            d => d.IsActive,
            opt => opt.MapFrom(s =>
                s.StartDate <= DateTime.UtcNow &&
                (!s.EndDate.HasValue || DateTime.UtcNow <= s.EndDate.Value)
            )
        )
        .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings))
        .ForMember(d => d.RateBands, opt => opt.MapFrom(s => s.RateBands));

            // --------------------------------
            // Create DTO → Entity
            // --------------------------------
            // Use ConstructUsing to call the aggregate constructor with nested maps.
            // Ignore read-only RateBands so AutoMapper doesn't try to mutate the collection after construction.
            CreateMap<DriverContractCreateDto, DriverContract>()
                .ConstructUsing((src, ctx) =>
                    new DriverContract(
                        src.DriverId,
                        ctx.Mapper.Map<DriverSettings>(src.Settings),
                        ctx.Mapper.Map<List<RateBand>>(src.RateBands),
                        src.StartDate,
                        src.EndDate
                    )
                )
                .ForMember(dest => dest.RateBands, opt => opt.Ignore())
                .ForMember(dest => dest.Settings, opt => opt.Ignore()); // already supplied via constructor

            // --------------------------------
            // Update DTO → Entity
            // --------------------------------
            // Only map non-null values to support partial updates (patch semantics).
            // Be cautious with nested immutables; prefer domain methods for updates if needed.
            CreateMap<DriverContractUpdateDto, DriverContract>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
