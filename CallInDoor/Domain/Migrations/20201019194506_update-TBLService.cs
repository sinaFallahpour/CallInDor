using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class updateTBLService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Service",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_RoleId",
                table: "Service",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_AspNetRoles_RoleId",
                table: "Service",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_AspNetRoles_RoleId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_RoleId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Service");
        }
    }
}
