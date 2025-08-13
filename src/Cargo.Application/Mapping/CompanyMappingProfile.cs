using AutoMapper;
using Cargo.Application.DTOs.Company;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
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
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => FormatAddress(src.Address)))
                // NOTE: If you expose a stable identifier/name on TaxProfile, map it here instead of hardcoding.
                .ForMember(dest => dest.TaxProfile, opt => opt.MapFrom(src => "Quebec"))
                .ForMember(dest => dest.DriverIds, opt => opt.MapFrom(src =>
                    src.Drivers != null ? src.Drivers.Select(d => d.Id).ToList() : new List<Guid>()))
                .ForMember(dest => dest.VehicleIds, opt => opt.MapFrom(src =>
                    src.Vehicles != null ? src.Vehicles.Select(v => v.Id).ToList() : new List<Guid>()));

            // CompanyCreateDto -> Company
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => ParseAddress(src.Address)))
                .ForMember(dest => dest.TaxProfile, opt => opt.MapFrom(src => CreateTaxProfile(src.TaxProfile)))
                .ForMember(dest => dest.Drivers, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore())
                // Ignore audit fields
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore()) // ⬅ Added
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
                    opt.MapFrom(src => ParseAddress(src.Address));
                })
                .ForMember(dest => dest.TaxProfile, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.TaxProfile));
                    opt.MapFrom(src => CreateTaxProfile(src.TaxProfile));
                })
                .AfterMap((src, dest) =>
                {
                    dest.UpdatedAt = DateTime.UtcNow;
                    dest.UpdatedBy = "System";
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

        private static string FormatAddress(Address address)
        {
            if (address == null) return string.Empty;

            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(address.Street)) parts.Add(address.Street);
            if (!string.IsNullOrWhiteSpace(address.City)) parts.Add(address.City);
            if (!string.IsNullOrWhiteSpace(address.State)) parts.Add(address.State);
            if (!string.IsNullOrWhiteSpace(address.ZipCode)) parts.Add(address.ZipCode);
            if (!string.IsNullOrWhiteSpace(address.Country)) parts.Add(address.Country);

            return string.Join(", ", parts);
        }

        private static Address ParseAddress(string addressString)
        {
            if (string.IsNullOrWhiteSpace(addressString))
                return new Address();

            var parts = addressString.Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToArray();

            var address = new Address();

            if (parts.Length >= 1) address.Street = parts[0];
            if (parts.Length >= 2) address.City = parts[1];
            if (parts.Length >= 3) address.State = parts[2];
            if (parts.Length >= 4) address.ZipCode = parts[3];
            if (parts.Length >= 5) address.Country = parts[4];

            return address;
        }

        private static TaxProfile CreateTaxProfile(string taxProfileString)
        {
            if (string.IsNullOrWhiteSpace(taxProfileString))
                return TaxProfile.CreateQuebecProfile();

            var value = taxProfileString.Trim();

            if (value.Equals("Quebec", StringComparison.OrdinalIgnoreCase))
                return TaxProfile.CreateQuebecProfile();

            if (value.Equals("Ontario", StringComparison.OrdinalIgnoreCase))
                return TaxProfile.CreateOntarioProfile();

            // Default/fallback
            return TaxProfile.CreateQuebecProfile();
        }
    }
}