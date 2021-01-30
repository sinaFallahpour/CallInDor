using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addsomeFieldToFirmTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirmDateOfRegistration",
                table: "FirmProfile",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmNationalID",
                table: "FirmProfile",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmRegistrationID",
                table: "FirmProfile",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirmDateOfRegistration",
                table: "FirmProfile");

            migrationBuilder.DropColumn(
                name: "FirmNationalID",
                table: "FirmProfile");

            migrationBuilder.DropColumn(
                name: "FirmRegistrationID",
                table: "FirmProfile");
        }
    }
}
