using AutoMapper;
using Cargo.Application.Mapping;

namespace Cargo.Application.Mapping
{
    /// <summary>
    /// Main AutoMapper profile that consolidates all transportation domain mappings
    /// </summary>
    public class TransportationMappingProfile : Profile
    {
        public TransportationMappingProfile()
        {
            // Include all individual mapping profiles
            Include<CommonMappingProfile>();
            Include<CompanyMappingProfile>();
            Include<DriverMappingProfile>();
            Include<VehicleMappingProfile>();
            Include<RouteMappingProfile>();
            Include<DriverBatchMappingProfile>();
        }
    }
}
