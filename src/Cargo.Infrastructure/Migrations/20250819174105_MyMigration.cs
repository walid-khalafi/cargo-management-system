using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerCompanyId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentLocation",
                table: "Vehicles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerCompanyId",
                table: "Vehicles",
                column: "OwnerCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerCompanyId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentLocation",
                table: "Vehicles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerCompanyId",
                table: "Vehicles",
                column: "OwnerCompanyId",
                unique: true);
        }
    }
}
