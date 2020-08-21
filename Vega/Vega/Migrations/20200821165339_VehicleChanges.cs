using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class VehicleChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Email",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Vehicles",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ContactEmail",
                table: "Vehicles",
                column: "ContactEmail",
                unique: true,
                filter: "[ContactEmail] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ContactEmail",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vehicles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Email",
                table: "Vehicles",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }
    }
}
