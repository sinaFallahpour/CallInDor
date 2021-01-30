using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addTransalatin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreeMessageCount",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreeUsageMessageCount",
                table: "ServiceRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeMessageCount",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "FreeUsageMessageCount",
                table: "ServiceRequest");
        }
    }
}
