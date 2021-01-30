using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class deleteBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Service_ServiceTBLId",
                table: "ServiceComments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceComments_ServiceTBLId",
                table: "ServiceComments");

            migrationBuilder.DropColumn(
                name: "ServiceTBLId",
                table: "ServiceComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTBLId",
                table: "ServiceComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComments_ServiceTBLId",
                table: "ServiceComments",
                column: "ServiceTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Service_ServiceTBLId",
                table: "ServiceComments",
                column: "ServiceTBLId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
