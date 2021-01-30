using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class fixSomBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPerodedOrsesionChat",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CheckDiscount",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckDiscount_ServiceId",
                table: "CheckDiscount",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckDiscount_Service_ServiceId",
                table: "CheckDiscount",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckDiscount_Service_ServiceId",
                table: "CheckDiscount");

            migrationBuilder.DropIndex(
                name: "IX_CheckDiscount_ServiceId",
                table: "CheckDiscount");

            migrationBuilder.DropColumn(
                name: "IsPerodedOrsesionChat",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CheckDiscount");
        }
    }
}
