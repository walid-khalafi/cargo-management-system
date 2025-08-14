using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.DriverBatches;
using Cargo.Application.DTOs.DriverBatches.Cargo.Application.DTOs;
using Cargo.Application.Mapping;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;
using Xunit;

namespace Cargo.Application.Tests.Domain
{
    public class DriverBatchMappingProfileTests
    {
        private readonly IMapper _mapper;

        public DriverBatchMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DriverBatchMappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }


        /// <summary>
        /// Validates mapping from DriverBatchWaitCreateDto (creation DTO) to the DriverBatchWait domain entity.
        /// Ensures that all provided values — including identifiers, wait type, duration, rate per minute, and multiplier —
        /// are accurately transferred. Verifies that monetary fields (RawPay and FinalPay) are rounded to two decimal places
        /// using the domain’s configured rounding strategy (AwayFromZero).
        /// This guards against mapping misconfigurations or changes in domain-side rounding logic for wait-time pay calculations.
        /// </summary>
        [Fact]
        public void Should_map_DriverBatchWaitCreateDto_to_entity()
        {
            // Arrange
            // Given a create DTO for a wait record with specific minute count, rate, multiplier, and unrounded pay values
            var dto = new DriverBatchWaitCreateDto
            {
                DarNumber = "DAR-001",
                CpPoNumber = "CP-PO-42",
                WaitType = WaitType.CustomerAccWait,
                WaitMinutes = 45,
                RatePerMinute = 0.75m,
                Multiplier = 1.2m,
                RawPayFromInvoice = 12.345m,
                FinalPayFromInvoice = 10.005m,
                DriverBatchId = Guid.NewGuid()
            };

            // Act
            // When mapping the DTO to the domain entity using the configured IMapper
            var entity = _mapper.Map<DriverBatchWait>(dto);

            // Assert
            // Then base fields should match exactly
            Assert.Equal(dto.DarNumber, entity.DarNumber);
            Assert.Equal(dto.CpPoNumber, entity.CpPoNumber);
            Assert.Equal(dto.WaitType, entity.WaitType);
            Assert.Equal(dto.WaitMinutes, entity.WaitMinutes);
            Assert.Equal(dto.RatePerMinute, entity.RatePerMinute);
            Assert.Equal(dto.Multiplier, entity.Multiplier);

            // And monetary values should be rounded AwayFromZero to two decimal places by the entity logic
            Assert.Equal(12.35m, entity.RawPay);
            Assert.Equal(10.01m, entity.FinalPay);
        }


        /// <summary>
        /// Validates one-way mapping from DriverBatchWait (domain entity) to DriverBatchWaitDto.
        /// Confirms that all wait-related attributes — including identifiers, wait type, duration, 
        /// pay rate, multiplier, and both raw and final pay values — are accurately transferred 
        /// via the mapping configuration.
        /// This test safeguards against mapping regressions that could cause discrepancies in 
        /// pay computation, reporting, or downstream processing for driver wait time records.
        /// </summary>
        [Fact]
        public void Should_map_DriverBatchWait_entity_to_dto()
        {
            // Arrange
            // Given a driver batch wait entity with complete wait details and no invoice overrides
            var wait = new DriverBatchWait(
                darNumber: "DAR-777",
                cpPoNumber: "CP-PO-99",
                waitType: WaitType.CustomerAccWait,
                waitMinutes: 30,
                ratePerMinute: 1.00m,
                multiplier: 1.0m,
                rawPayFromInvoice: null,
                finalPayFromInvoice: null
            );

            // Act
            // When mapping the entity to its DTO representation via the configured IMapper
            var dto = _mapper.Map<DriverBatchWaitDto>(wait);

            // Assert
            // Then all base and calculated properties are preserved
            Assert.Equal(wait.DarNumber, dto.DarNumber);
            Assert.Equal(wait.CpPoNumber, dto.CpPoNumber);
            Assert.Equal(wait.WaitType, dto.WaitType);
            Assert.Equal(wait.WaitMinutes, dto.WaitMinutes);
            Assert.Equal(wait.RatePerMinute, dto.RatePerMinute);
            Assert.Equal(wait.Multiplier, dto.Multiplier);
            Assert.Equal(wait.RawPay, dto.RawPay);
            Assert.Equal(wait.FinalPay, dto.FinalPay);
        }


        /// <summary>
        /// Validates mapping from DriverBatchLoadCreateDto (creation DTO) to the DriverBatchLoad domain entity.
        /// Ensures all load, routing, and rate-related fields are correctly transferred, while verifying that
        /// monetary values subject to rounding — such as FSC pay and temporary emergency fuel pay — are rounded 
        /// according to domain rules in the entity constructor.
        /// Also confirms that base pay and net pay calculations within the entity are accurate for the PerMile rate type:
        /// (LegMiles × Rate) + FscPay + TemporaryEmergencyFuelPay.
        /// This test guards against mapping misconfigurations and regression in domain-side calculation or rounding logic.
        /// </summary>
        [Fact]
        public void Should_map_DriverBatchLoadCreateDto_to_entity()
        {
            // Arrange
            // Given a create DTO containing identifiers, origin/destination postal codes, distance, load and rate types,
            // and unrounded fuel-related pay values to test entity-side rounding behavior
            var dto = new DriverBatchLoadCreateDto
            {
                DarNumber = "DAR-100",
                LoadNumber = "LD-200",
                OriginPc = "H1A1A1",
                DestinationPc = "H2B2B2",
                LegMiles = 120,
                LoadType = LoadType.Trailer,
                RateType = RateType.PerMile,
                Rate = 0.50m,
                BandLabel = "Band A",
                FscPay = 15.123m,
                TemporaryEmergencyFuelPay = 2.555m,
                DriverBatchId = Guid.NewGuid()
            };

            // Act
            // When mapping the create DTO to the domain entity via the configured IMapper
            var entity = _mapper.Map<DriverBatchLoad>(dto);

            // Assert
            // Then all base properties are preserved
            Assert.Equal(dto.DarNumber, entity.DarNumber);
            Assert.Equal(dto.LoadNumber, entity.LoadNumber);
            Assert.Equal(dto.OriginPc, entity.OriginPc);
            Assert.Equal(dto.DestinationPc, entity.DestinationPc);
            Assert.Equal(dto.LegMiles, entity.LegMiles);
            Assert.Equal(dto.LoadType, entity.LoadType);
            Assert.Equal(dto.RateType, entity.RateType);
            Assert.Equal(dto.Rate, entity.Rate);
            Assert.Equal(dto.BandLabel, entity.BandLabel);

            // And rounding is applied by the domain constructor
            Assert.Equal(15.12m, entity.FscPay);
            Assert.Equal(2.56m, entity.TemporaryEmergencyFuelPay);

            // And calculated base pay (PerMile: miles × rate) plus fuel adjustments equals net pay
            // Here, base pay: 120 × 0.50 = 60.00
            Assert.Equal(60.00m, entity.CalculateNetPay() - entity.FscPay - entity.TemporaryEmergencyFuelPay);
        }


        /// <summary>
        /// Validates one-way mapping from DriverBatchLoad (domain entity) to DriverBatchLoadDto.
        /// Ensures all load-related details — including identifiers, postal codes, mileage, load and rate types,
        /// base rate, band label, fuel surcharges, and temporary emergency fuel pay — are correctly transferred.
        /// Also asserts that the DTO's NetWefp value matches the entity's calculated net pay,
        /// protecting against mapping profile errors that could affect pay accuracy or downstream reporting.
        /// </summary>
        [Fact]
        public void Should_map_DriverBatchLoad_entity_to_dto()
        {
            // Arrange
            // Given a driver batch load record with full set of load, route, and pay information
            var load = new DriverBatchLoad(
                darNumber: "DAR-300",
                loadNumber: "LD-400",
                originPc: "A1A1A1",
                destinationPc: "B2B2B2",
                legMiles: 100,
                loadType: LoadType.Trailer,
                rateType: RateType.Flat,
                rate: 250.00m,
                bandLabel: "Band B",
                fscPay: 20.00m,
                temporaryEmergencyFuelPay: 5.00m
            );

            // Act
            // When mapping the entity to its DTO representation via the configured IMapper
            var dto = _mapper.Map<DriverBatchLoadDto>(load);

            // Assert
            // Then all primitive and enum properties should be preserved
            Assert.Equal(load.DarNumber, dto.DarNumber);
            Assert.Equal(load.LoadNumber, dto.LoadNumber);
            Assert.Equal(load.OriginPc, dto.OriginPc);
            Assert.Equal(load.DestinationPc, dto.DestinationPc);
            Assert.Equal(load.LegMiles, dto.LegMiles);
            Assert.Equal(load.LoadType, dto.LoadType);
            Assert.Equal(load.RateType, dto.RateType);
            Assert.Equal(load.Rate, dto.Rate);
            Assert.Equal(load.BandLabel, dto.BandLabel);
            Assert.Equal(load.FscPay, dto.FscPay);
            Assert.Equal(load.TemporaryEmergencyFuelPay, dto.TemporaryEmergencyFuelPay);

            // And the computed net pay in the DTO should match the entity's calculation
            Assert.Equal(load.CalculateNetPay(), dto.NetWefp);
        }


        /// <summary>
        /// Validates mapping from DriverBatchHourlyCreateDto (input DTO used for creating records)
        /// to the DriverBatchHourly domain entity. Ensures all provided values — including date, 
        /// worked time (hours/minutes), and rate per hour — are transferred correctly, and that 
        /// total pay is calculated according to business rules when not explicitly supplied.
        /// In this scenario, null TotalPayFromInvoice triggers an internal calculation:
        /// (Hours + Minutes/60) × RatePerHour, with proper decimal precision for currency.
        /// </summary>
        [Fact]
        public void Should_map_DriverBatchHourlyCreateDto_to_entity()
        {
            // Arrange
            // Given a DTO representing a new hourly batch entry, with no explicit total pay provided
            // (to test entity-side calculation logic)
            var dto = new DriverBatchHourlyCreateDto
            {
                Date = new DateTime(2025, 1, 15),
                Hours = 2,
                Minutes = 30,
                RatePerHour = 40.00m,
                TotalPayFromInvoice = null,
                DriverBatchId = Guid.NewGuid()
            };

            // Act
            // When mapping the create DTO to the domain entity via the configured IMapper
            var entity = _mapper.Map<DriverBatchHourly>(dto);

            // Assert
            // Then the basic fields should be identical
            Assert.Equal(dto.Date, entity.Date);
            Assert.Equal(dto.Hours, entity.Hours);
            Assert.Equal(dto.Minutes, entity.Minutes);
            Assert.Equal(dto.RatePerHour, entity.RatePerHour);

            // And total pay should be computed: 2.5 hours × 40.00 = 100.00
            Assert.Equal(100.00m, entity.TotalPay);
        }


        /// <summary>
        /// Validates one-way mapping from DriverBatchHourly (domain entity) to DriverBatchHourlyDto.
        /// Confirms that date, time components, hourly rate, and computed total pay values are correctly 
        /// transferred and rounded according to business rules (currency rounding to two decimal places).
        /// This protects against mapping profile changes that could distort worked time, pay rates, 
        /// or rounding logic in financial calculations.
        /// </summary>
        [Fact]
        public void Should_map_DriverBatchHourly_entity_to_dto()
        {
            // Arrange
            // Given a driver batch record for a specific date with worked hours/minutes and rate per hour
            // Total pay from invoice contains a fractional cent to test currency rounding behavior
            var hourly = new DriverBatchHourly(
                date: new DateTime(2025, 2, 1),
                hours: 1,
                minutes: 15,
                ratePerHour: 32.00m,
                totalPayFromInvoice: 40.005m // should round to 40.01
            );

            // Act
            // When mapping the entity to its DTO representation via the configured IMapper
            var dto = _mapper.Map<DriverBatchHourlyDto>(hourly);

            // Assert
            // Then all primitive fields are preserved
            Assert.Equal(hourly.Date, dto.Date);
            Assert.Equal(hourly.Hours, dto.Hours);
            Assert.Equal(hourly.Minutes, dto.Minutes);
            Assert.Equal(hourly.RatePerHour, dto.RatePerHour);

            // And total pay is correctly rounded to two decimal places in the DTO
            Assert.Equal(40.01m, dto.TotalPay);
        }



        /// <summary>
        /// Verifies bi-directional mapping between TaxAmounts (domain entity) and TaxAmountsDto.
        /// Ensures that GST, QST, PST, and HST monetary values are preserved without loss or transformation
        /// when mapping entity -> DTO and DTO -> entity. This protects against configuration drift in the
        /// mapping profile and guards precision for currency fields.
        /// </summary>
        [Fact]
        public void Should_map_TaxAmounts_entity_to_dto_and_back()
        {
            // Arrange
            // Given a domain entity with explicit monetary values across all tax components
            var amounts = new TaxAmounts
            {
                GstAmount = 5.00m,
                QstAmount = 9.98m,
                PstAmount = 0.00m,
                HstAmount = 0.00m
            };

            // Act
            // When mapping entity -> DTO and then DTO -> entity using the configured IMapper
            var dto = _mapper.Map<TaxAmountsDto>(amounts);
            var back = _mapper.Map<TaxAmounts>(dto);

            // Assert
            // Then all tax amounts are identical after the first hop (entity -> DTO)
            Assert.Equal(amounts.GstAmount, dto.GstAmount);
            Assert.Equal(amounts.QstAmount, dto.QstAmount);
            Assert.Equal(amounts.PstAmount, dto.PstAmount);
            Assert.Equal(amounts.HstAmount, dto.HstAmount);

            // And remain identical after the round-trip (DTO -> entity)
            Assert.Equal(dto.GstAmount, back.GstAmount);
            Assert.Equal(dto.QstAmount, back.QstAmount);
            Assert.Equal(dto.PstAmount, back.PstAmount);
            Assert.Equal(dto.HstAmount, back.HstAmount);
        }

    }
}
