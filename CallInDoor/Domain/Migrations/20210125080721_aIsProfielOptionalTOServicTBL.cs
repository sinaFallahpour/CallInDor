using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class aIsProfielOptionalTOServicTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProfileOptional",
                table: "Service",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileOptional",
                table: "BaseMyService",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileOptional",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsProfileOptional",
                table: "BaseMyService");
        }
    }
}
