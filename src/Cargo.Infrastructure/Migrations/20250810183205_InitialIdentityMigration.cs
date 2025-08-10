using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaxProfile_GstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_QstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_PstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_HstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_CompoundQstOverGst = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Waypoints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDistance = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    EstimatedDuration = table.Column<TimeSpan>(type: "time(5)", precision: 5, scale: 2, nullable: false),
                    EstimatedFuelCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedTollCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RouteType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Settings_NumPayBands = table.Column<int>(type: "int", nullable: false),
                    Settings_HourlyRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Settings_FscRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Settings_FscMode = table.Column<int>(type: "int", nullable: false),
                    Settings_WaitingPerMinute = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Settings_AdminFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Settings_Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Settings_TaxProfile_GstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Settings_TaxProfile_QstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Settings_TaxProfile_PstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Settings_TaxProfile_HstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Settings_TaxProfile_CompoundQstOverGst = table.Column<bool>(type: "bit", nullable: false),
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drivers_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlateNumber_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PlateNumber_IssuingAuthority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlateNumber_PlateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    CurrentLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_OwnerCompanyId",
                        column: x => x.OwnerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DriverContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumPayBands = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FscRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FscMode = table.Column<int>(type: "int", nullable: false),
                    WaitingPerMinute = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GstRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QstRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PstRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    HstRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CompoundQstOverGst = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverContracts_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverVehicleAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverRole = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverVehicleAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverVehicleAssignments_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverVehicleAssignments_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleOwnerships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OwnedFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleOwnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleOwnerships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleOwnerships_Companies_OwnerCompanyId",
                        column: x => x.OwnerCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleOwnerships_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateBands",
                columns: table => new
                {
                    Band = table.Column<int>(type: "int", nullable: false),
                    LoadType = table.Column<int>(type: "int", nullable: false),
                    DriverContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinMiles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxMiles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContainerRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlatbedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateBands", x => new { x.DriverContractId, x.Band, x.LoadType });
                    table.ForeignKey(
                        name: "FK_RateBands_DriverContracts_DriverContractId",
                        column: x => x.DriverContractId,
                        principalTable: "DriverContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverBatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatementStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatementEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleOwnershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnershipTypeAtBatch = table.Column<int>(type: "int", nullable: false),
                    DrayPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DrayFsc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TemporaryEmergencyFsc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaitingPayoutPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DriverSharePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    AdminFeePercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    AdminFeeFlat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminFeeAppliesBeforeTaxes = table.Column<bool>(type: "bit", nullable: false),
                    AdjustmentsTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxProfile_GstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_QstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_PstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_HstRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    TaxProfile_CompoundQstOverGst = table.Column<bool>(type: "bit", nullable: false),
                    TripTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaitingRawTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaitingTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HourlyTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaitPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverShareAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes_GstAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes_QstAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes_PstAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes_HstAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverBatches_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverBatches_VehicleOwnerships_VehicleOwnershipId",
                        column: x => x.VehicleOwnershipId,
                        principalTable: "VehicleOwnerships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverBatchHourlies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    RatePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverBatchHourlies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverBatchHourlies_DriverBatches_DriverBatchId",
                        column: x => x.DriverBatchId,
                        principalTable: "DriverBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverBatchLoads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoadNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OriginPc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DestinationPc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LegMiles = table.Column<int>(type: "int", nullable: false),
                    LoadType = table.Column<int>(type: "int", nullable: false),
                    RateType = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BandLabel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BasePay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FscPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TemporaryEmergencyFuelPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetWefp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverBatchLoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverBatchLoads_DriverBatches_DriverBatchId",
                        column: x => x.DriverBatchId,
                        principalTable: "DriverBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverBatchWaits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DarNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CpPoNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WaitType = table.Column<int>(type: "int", nullable: false),
                    WaitMinutes = table.Column<int>(type: "int", nullable: false),
                    RatePerMinute = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Multiplier = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    RawPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverBatchWaits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverBatchWaits_DriverBatches_DriverBatchId",
                        column: x => x.DriverBatchId,
                        principalTable: "DriverBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatches_BatchNumber",
                table: "DriverBatches",
                column: "BatchNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatches_DriverId",
                table: "DriverBatches",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatches_StatementEndDate",
                table: "DriverBatches",
                column: "StatementEndDate");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatches_StatementStartDate",
                table: "DriverBatches",
                column: "StatementStartDate");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatches_Status",
                table: "DriverBatches",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatches_VehicleOwnershipId",
                table: "DriverBatches",
                column: "VehicleOwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatchHourlies_DriverBatchId",
                table: "DriverBatchHourlies",
                column: "DriverBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatchLoads_DriverBatchId",
                table: "DriverBatchLoads",
                column: "DriverBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatchLoads_LoadNumber",
                table: "DriverBatchLoads",
                column: "LoadNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatchLoads_LoadType",
                table: "DriverBatchLoads",
                column: "LoadType");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatchWaits_DriverBatchId",
                table: "DriverBatchWaits",
                column: "DriverBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverBatchWaits_WaitType",
                table: "DriverBatchWaits",
                column: "WaitType");

            migrationBuilder.CreateIndex(
                name: "IX_DriverContracts_DriverId",
                table: "DriverContracts",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CompanyId",
                table: "Drivers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Email",
                table: "Drivers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LicenseNumber",
                table: "Drivers",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RouteId",
                table: "Drivers",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicleAssignments_DriverId",
                table: "DriverVehicleAssignments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicleAssignments_DriverId_VehicleId_AssignedAt",
                table: "DriverVehicleAssignments",
                columns: new[] { "DriverId", "VehicleId", "AssignedAt" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicleAssignments_VehicleId",
                table: "DriverVehicleAssignments",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerships_CompanyId",
                table: "VehicleOwnerships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerships_OwnerCompanyId",
                table: "VehicleOwnerships",
                column: "OwnerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerships_VehicleId",
                table: "VehicleOwnerships",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerships_VehicleId_OwnerCompanyId_OwnedFrom",
                table: "VehicleOwnerships",
                columns: new[] { "VehicleId", "OwnerCompanyId", "OwnedFrom" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverId",
                table: "Vehicles",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerCompanyId",
                table: "Vehicles",
                column: "OwnerCompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RegistrationNumber",
                table: "Vehicles",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RouteId",
                table: "Vehicles",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VIN",
                table: "Vehicles",
                column: "VIN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DriverBatchHourlies");

            migrationBuilder.DropTable(
                name: "DriverBatchLoads");

            migrationBuilder.DropTable(
                name: "DriverBatchWaits");

            migrationBuilder.DropTable(
                name: "DriverVehicleAssignments");

            migrationBuilder.DropTable(
                name: "RateBands");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DriverBatches");

            migrationBuilder.DropTable(
                name: "DriverContracts");

            migrationBuilder.DropTable(
                name: "VehicleOwnerships");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
