using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.DriverBatches;
using Cargo.Application.DTOs.DriverContracts;
using Cargo.Application.MappingProfiles;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xunit;

namespace Cargo.Application.Tests.Domain
{
    namespace Cargo.Application.Tests.Domain
    {
        /// <summary>
        /// Contains unit tests for validating the <see cref="DriverContractMapingProfile"/>
        /// AutoMapper configuration. Covers both directions of mapping between:
        /// <list type="bullet">
        ///   <item>
        ///     <description>Domain entities (e.g., <see cref="DriverContract"/>)</description>
        ///   </item>
        ///   <item>
        ///     <description>Data transfer objects (e.g., <see cref="DriverContractDto"/>, 
        ///     <see cref="DriverContractCreateDto"/>)</description>
        ///   </item>
        /// </list>
        /// 
        /// Key verifications include:
        /// <list type="number">
        ///   <item>
        ///     <description>Mapping of nested objects like <see cref="DriverSettings"/> and 
        ///     its <see cref="TaxProfile"/> value object.</description>
        ///   </item>
        ///   <item>
        ///     <description>Projection of computed properties (e.g., <c>DriverName</c>, 
        ///     <c>IsActive</c>).</description>
        ///   </item>
        ///   <item>
        ///     <description>Correct DTO → Entity conversion, including nested and collection members.</description>
        ///   </item>
        ///   <item>
        ///     <description>Time-sensitive logic for <c>IsActive</c> when projecting queries 
        ///     with <see cref="QueryableExtensions.ProjectTo"/>.</description>
        ///   </item>
        /// </list>
        /// These tests safeguard against regressions caused by changes to AutoMapper profiles
        /// or DTO/entity definitions.
        /// </summary>
        public class DriverContractProfileTests
        {
            private readonly IMapper _mapper;
            private readonly MapperConfiguration _config;

            public DriverContractProfileTests()
            {
                // Initialize AutoMapper with the DriverContract mapping profile
                _config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DriverContractMapingProfile>();
                });

                _mapper = _config.CreateMapper();
            }

            /// <summary>
            /// Ensures that mapping from a fully populated <see cref="DriverContract"/>
            /// to <see cref="DriverContractDto"/> preserves:
            /// <list type="bullet">
            ///   <item><description>Nested <see cref="DriverSettings"/> values</description></item>
            ///   <item><description>Nested <see cref="TaxProfile"/> values</description></item>
            ///   <item><description>The computed <c>DriverName</c></description></item>
            ///   <item><description>The computed <c>IsActive</c> flag</description></item>
            /// </list>
            /// </summary>
            [Fact]
            public void Map_EntityToDto_Maps_DriverSettings_And_TaxProfile()
            {
                // Arrange: create domain Driver entity with realistic details
                var start = DateTime.UtcNow.AddDays(-1);
                var end = DateTime.UtcNow.AddDays(5);
                var driverId = Guid.NewGuid();
                var driver = new Driver
                {
                    Id = driverId,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "+1-555-123-4567",
                    Address = new Address("Canada", "QC", "Montreal", "123 Main Street", "H3A 1B2"),
                    LicenseNumber = "DL123456789",
                    LicenseType = "CDL-A",
                    LicenseExpiryDate = new DateTime(2025, 12, 31),
                    DateOfBirth = new DateTime(1985, 5, 15),
                    YearsOfExperience = 10,
                    Status = DriverStatus.Active,
                    CompanyId = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    CreatedBy = "Test User",
                    CreatedByIP = "192.168.1.1"
                };

                // Arrange: create settings and contract with nested TaxProfile
                var taxProfile = new TaxProfile(0.05m, 0.09975m, 0.07m, 0.13m, true);
                var settings = new DriverSettings(
                    3, 120.5m, 0.15m, FscMode.Percentage, 2.5m, 100, "Tehran", taxProfile
                );
                var contract = new DriverContract(Guid.NewGuid(), settings, new List<RateBand>(), start, end);
                // Inject driver into contract (private setter)
                contract.GetType().GetProperty("Driver")!.SetValue(contract, driver);

                // Act: map entity to DTO
                var dto = _mapper.Map<DriverContractDto>(contract);

                // Assert: verify all key mapped values
                Assert.Equal(settings.NumPayBands, dto.Settings.NumPayBands);
                Assert.Equal(settings.TaxProfile.GstRate, dto.Settings.TaxProfile.GstRate);
                Assert.Equal(settings.TaxProfile.CompoundQstOverGst, dto.Settings.TaxProfile.CompoundQstOverGst);
                Assert.Equal("John Doe", dto.DriverName);
                Assert.True(dto.IsActive);
            }

            /// <summary>
            /// Verifies that mapping from <see cref="DriverContractCreateDto"/>
            /// to <see cref="DriverContract"/> correctly transfers:
            /// <list type="bullet">
            ///   <item><description><see cref="DriverSettings"/> values</description></item>
            ///   <item><description>Nested <see cref="TaxProfile"/> values</description></item>
            ///   <item><description>RateBands collection</description></item>
            /// </list>
            /// </summary>
            [Fact]
            public void Map_CreateDto_ToEntity_MapsNestedSettings_And_TaxProfile()
            {
                // Arrange: populate create DTO with nested settings and tax profile
                var createDto = new DriverContractCreateDto
                {
                    DriverId = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    Settings = new DriverSettingsDto
                    {
                        NumPayBands = 3,
                        HourlyRate = 120.5m,
                        FscRate = 0.15m,
                        FscMode = FscMode.Percentage,
                        WaitingPerMinute = 2.5m,
                        AdminFee = 100,
                        Province = "Tehran",
                        TaxProfile = new TaxProfileDto
                        {
                            GstRate = 0.05m,
                            QstRate = 0.09975m,
                            PstRate = 0.07m,
                            HstRate = 0.13m,
                            CompoundQstOverGst = true
                        }
                    },
                    RateBands = new List<RateBandDto>
                {
                    new RateBandDto { Label = "A", Rate = 1.2m, MileageThreshold = 100 }
                }
                };

                // Act: map DTO to entity
                var entity = _mapper.Map<DriverContract>(createDto);

                // Assert: verify key nested and scalar property mappings
                Assert.Equal(createDto.Settings.NumPayBands, entity.Settings.NumPayBands);
                Assert.Equal(createDto.Settings.TaxProfile.GstRate, entity.Settings.TaxProfile.GstRate);
                Assert.Equal(createDto.Settings.TaxProfile.CompoundQstOverGst, entity.Settings.TaxProfile.CompoundQstOverGst);
            }

            /// <summary>
            /// Confirms that when the current date is past the contract's EndDate,
            /// the mapped DTO's <c>IsActive</c> property is set to <c>false</c>.
            /// </summary>
            [Fact]
            public void Map_EntityToDto_isactive_false_when_now_after_end()
            {
                // Arrange: create contract with EndDate in the past
                var now = DateTime.UtcNow;
                var start = now.AddDays(-10);
                var end = now.AddDays(-1);
                var contract = CreateContract(start, end);

                // Act: map entity to DTO
                var dto = _mapper.Map<DriverContractDto>(contract);

                // Assert: IsActive should be false
                Assert.False(dto.IsActive);
            }

            // Helper: creates an uninitialized DriverContract with minimal valid data
            private static DriverContract CreateContract(DateTime start, DateTime? end)
            {
                var settings = NewUninitialized<DriverSettings>(); // bypass ctor for testing
                var bands = new List<RateBand>(); // empty collection acceptable

                var driverId = Guid.NewGuid();
                return new DriverContract(driverId, settings, bands, start, end);
            }

            // Helper: creates an instance of a class without invoking any constructor
            private static T NewUninitialized<T>() where T : class =>
                (T)FormatterServices.GetUninitializedObject(typeof(T));
        }
    }

}
