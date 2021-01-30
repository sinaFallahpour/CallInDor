using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class nj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopTenPackage_Service_ServiceId",
                table: "TopTenPackage");

            migrationBuilder.DropIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_TopTenPackage_TopTenPackageId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_TopTenPackageId",
                table: "Service");

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId",
                unique: true,
                filter: "[ServiceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TopTenPackage_Service_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
