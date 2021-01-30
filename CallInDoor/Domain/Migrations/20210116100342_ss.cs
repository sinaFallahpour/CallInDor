using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service");

            migrationBuilder.AddColumn<int>(
                name: "TopTenPackageId",
                table: "TopTenPackage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service",
                column: "TopTenPackageId",
                unique: true,
                filter: "[TopTenPackageId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TopTenPackageId",
                table: "TopTenPackage");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service",
                column: "TopTenPackageId");
        }
    }
}
