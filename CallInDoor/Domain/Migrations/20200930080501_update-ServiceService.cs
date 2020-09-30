using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class updateServiceService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilrDescription",
                table: "MyServiceService");

            migrationBuilder.AddColumn<string>(
                name: "FileDescription",
                table: "MyServiceService",
                maxLength: 600,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDescription",
                table: "MyServiceService");

            migrationBuilder.AddColumn<string>(
                name: "FilrDescription",
                table: "MyServiceService",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true);
        }
    }
}
