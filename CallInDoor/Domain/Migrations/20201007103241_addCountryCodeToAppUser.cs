using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addCountryCodeToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "AspNetUsers");
        }
    }
}
