using AutoMapper;
using Cargo.Application.DTOs.Company;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using Cargo.Application.Mapping.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cargo.Application.Mapping
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            // Company -> CompanyDto
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => MappingHelper.FormatAddress(src.Address)))
                // NOTE: If you expose a stable identifier/name on TaxProfile, map it here instead of hardcoding.
                .ForMember(dest => dest.TaxProfile, opt => opt.MapFrom(src => MappingHelper.FormatTaxProfile(src.TaxProfile)))
                .ForMember(dest => dest.DriverIds, opt => opt.MapFrom(src =>
                    src.Drivers != null ? src.Drivers.Select(d => d.Id).ToList() : new List<Guid>()))
                .ForMember(dest => dest.VehicleIds, opt => opt.MapFrom(src =>
                    src.Vehicles != null ? src.Vehicles.Select(v => v.Id).ToList() : new List<Guid>()));

            // CompanyCreateDto -> Company
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => MappingHelper.ParseAddress(src.Address)))
                .ForMember(dest => dest.TaxProfile, opt => opt.MapFrom(src => MappingHelper.ParseTaxProfile(src.TaxProfile)))
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                // Ignore audit fields
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.CreatedAt = DateTime.UtcNow;
                    dest.CreatedBy = "System";
                    dest.CreatedByIP = "System";
                });

            // CompanyUpdateDto -> Company
            CreateMap<CompanyUpdateDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Address));
                    opt.MapFrom(src => MappingHelper.ParseAddress(src.Address));
                })
                .ForMember(dest => dest.TaxProfile, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.TaxProfile));
                    opt.MapFrom(src => MappingHelper.ParseTaxProfile(src.TaxProfile));
                })
                .AfterMap((src, dest) =>
                {
                    dest.UpdatedAt = DateTime.UtcNow;
                    dest.UpdatedBy = "System";
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }





    }
}