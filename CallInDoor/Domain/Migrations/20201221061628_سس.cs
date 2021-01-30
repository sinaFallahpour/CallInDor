using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class سس : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeWithDetails",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTypeWithDetails",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
