using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class ssdsds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_TopTenPackage_TopTenPackageId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage");

            migrationBuilder.DropIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "TopTenPackageId",
                table: "TopTenPackage");

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

            migrationBuilder.AddColumn<int>(
                name: "TopTenPackageId",
                table: "TopTenPackage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service",
                column: "TopTenPackageId",
                unique: true,
                filter: "[TopTenPackageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_TopTenPackage_TopTenPackageId",
                table: "Service",
                column: "TopTenPackageId",
                principalTable: "TopTenPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
