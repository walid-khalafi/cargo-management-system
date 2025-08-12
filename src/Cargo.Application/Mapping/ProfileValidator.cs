using AutoMapper;
using Cargo.Application.Mapping;

namespace Cargo.Application.Mapping
{
    public static class ProfileValidator
    {
        public static bool ValidateProfiles()
        {
            try
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<CommonMappingProfile>();
                    cfg.AddProfile<CompanyMappingProfile>();
                    cfg.AddProfile<DriverMappingProfile>();
                    cfg.AddProfile<VehicleMappingProfile>();
                    cfg.AddProfile<RouteMappingProfile>();
                    cfg.AddProfile<DriverBatchMappingProfile>();
                });

                configuration.AssertConfigurationIsValid();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
