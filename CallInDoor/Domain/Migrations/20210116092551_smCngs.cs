using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class smCngs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage");

            migrationBuilder.AddColumn<int>(
                name: "TopTenPackageId",
                table: "Service",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId",
                unique: true,
                filter: "[ServiceId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage");

            migrationBuilder.DropColumn(
                name: "TopTenPackageId",
                table: "Service");

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId");
        }
    }
}
