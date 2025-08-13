using AutoMapper;
using Cargo.Application.DTOs.Driver;
using Cargo.Application.Mapping.Helpers;
using Cargo.Domain.Entities;
using System;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between Driver entity and its DTO representations.
    /// Includes configurations for create, update, and read scenarios with audit safety.
    /// </summary>
    public class DriverMappingProfile : Profile
    {
        public DriverMappingProfile()
        {
            // Domain → DTO: Driver → DriverDto
            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => 
                    string.IsNullOrWhiteSpace(src.FirstName) && string.IsNullOrWhiteSpace(src.LastName) 
                        ? " " 
                        : $"{src.FirstName} {src.LastName}".Trim()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => MappingHelper.FormatAddress(src.Address)));

            // Create DTO → Domain: DriverCreateDto → Driver
            CreateMap<DriverCreateDto, Driver>()
                // Identity and navigation are managed by the domain/ORM
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Company, opt => opt.Ignore())

                // Address parsed from single string input
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => MappingHelper.ParseAddress(src.Address)))

                // Audit/system-managed fields are not mapped from DTO
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())

                // Set audit fields after map
                .AfterMap((src, dest) =>
                {
                    dest.CreatedAt = DateTime.UtcNow;
                    dest.CreatedBy = "System";
                    dest.CreatedByIP = "System";
                    dest.Id = Guid.Empty; // Ensure Id is ignored and set to default
                    dest.UpdatedAt = DateTime.MinValue; // Ensure UpdatedAt is set to default
                    dest.UpdatedBy = null;
                    dest.UpdatedByIP = null;
                });

            // Update DTO → Domain: DriverUpdateDto → Driver
            CreateMap<DriverUpdateDto, Driver>()
                // Identity and navigation are managed by the domain/ORM
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Company, opt => opt.Ignore())

                // Audit/system-managed fields are not mapped from DTO
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByIP, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedByIP, opt => opt.Ignore())

                // Address only if provided (preserve existing when null/whitespace)
                .ForMember(dest => dest.Address, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Address));
                    opt.MapFrom(src => MappingHelper.ParseAddress(src.Address));
                })
                .ForMember(dest => dest.FirstName, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.LastName, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.Email, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.PhoneNumber, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.LicenseNumber, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.LicenseType, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.LicenseExpiryDate, opt => opt.Condition((src, dest, srcMember) => src.LicenseExpiryDate.HasValue))
                .ForMember(dest => dest.DateOfBirth, opt => opt.Condition((src, dest, srcMember) => src.DateOfBirth.HasValue))
                .ForMember(dest => dest.YearsOfExperience, opt => opt.Condition((src, dest, srcMember) => src.YearsOfExperience.HasValue))
                .ForMember(dest => dest.Status, opt => opt.Condition((src, dest, srcMember) => src.Status.HasValue))
                .ForMember(dest => dest.CompanyId, opt => opt.Condition((src, dest, srcMember) => src.CompanyId.HasValue))
                // Set audit fields after map
                .AfterMap((src, dest) =>
                {
                    dest.UpdatedAt = DateTime.UtcNow;
                    dest.UpdatedBy = "System";
                });

               
        }
    }
}