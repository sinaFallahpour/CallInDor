using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addSetssionTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MinPriceForService",
                table: "Service",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinSessionTime",
                table: "Service",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinPriceForService",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "MinSessionTime",
                table: "Service");
        }
    }
}
