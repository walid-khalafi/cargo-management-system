using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.Company;
using Cargo.Application.DTOs.Driver;
using Cargo.Application.DTOs.DriverBatch;
using Cargo.Application.DTOs.Route;
using Cargo.Application.DTOs.Vehicle;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between domain entities and DTOs
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Common mappings
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            
            CreateMap<TaxProfile, TaxProfileDto>();
            CreateMap<TaxProfileDto, TaxProfile>();
            
            CreateMap<PlateNumber, PlateNumberDto>();
            CreateMap<PlateNumberDto, PlateNumber>();

            // Company mappings
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.DriverCount, opt => opt.MapFrom(src => src.Drivers.Count))
                .ForMember(dest => dest.VehicleCount, opt => opt.MapFrom(src => src.Vehicles.Count));
            
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<UpdateCompanyDto, Company>();

            // Driver mappings
            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            
            CreateMap<CreateDriverDto, Driver>();
            CreateMap<UpdateDriverDto, Driver>();

            // Vehicle mappings
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>();

            // Route mappings
            CreateMap<Route, RouteDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RouteType, opt => opt.MapFrom(src => src.RouteType.ToString()));
            
            CreateMap<CreateRouteDto, Route>();
            CreateMap<UpdateRouteDto, Route>();

            // DriverBatch mappings
            CreateMap<DriverBatch, DriverBatchDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            
            CreateMap<CreateDriverBatchDto, DriverBatch>();

            // DriverBatch entries
            CreateMap<DriverBatchHourly, DriverBatchHourlyDto>();
            CreateMap<DriverBatchLoad, DriverBatchLoadDto>();
            CreateMap<DriverBatchWait, DriverBatchWaitDto>();
        }
    }
}
