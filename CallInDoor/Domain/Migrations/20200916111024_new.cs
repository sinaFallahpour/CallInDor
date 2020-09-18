using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DegreeEnglishName",
                table: "User_Degree_Field");

            migrationBuilder.AddColumn<string>(
                name: "DegreePersianName",
                table: "User_Degree_Field",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DegreePersianName",
                table: "User_Degree_Field");

            migrationBuilder.AddColumn<string>(
                name: "DegreeEnglishName",
                table: "User_Degree_Field",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
