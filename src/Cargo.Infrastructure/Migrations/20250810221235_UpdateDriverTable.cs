using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDriverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings_AdminFee",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_FscMode",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_FscRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_HourlyRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_NumPayBands",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_Province",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_TaxProfile_CompoundQstOverGst",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_TaxProfile_GstRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_TaxProfile_HstRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_TaxProfile_PstRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_TaxProfile_QstRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Settings_WaitingPerMinute",
                table: "Drivers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Settings_AdminFee",
                table: "Drivers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Settings_FscMode",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_FscRate",
                table: "Drivers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_HourlyRate",
                table: "Drivers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Settings_NumPayBands",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Settings_Province",
                table: "Drivers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Settings_TaxProfile_CompoundQstOverGst",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_TaxProfile_GstRate",
                table: "Drivers",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_TaxProfile_HstRate",
                table: "Drivers",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_TaxProfile_PstRate",
                table: "Drivers",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_TaxProfile_QstRate",
                table: "Drivers",
                type: "decimal(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_WaitingPerMinute",
                table: "Drivers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
