using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class ChnageInPaynmetn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "ServiceRequest");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceTypeWithDetails",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceTypes",
                table: "ServiceRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTypes",
                table: "ServiceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeWithDetails",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "ServiceRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
