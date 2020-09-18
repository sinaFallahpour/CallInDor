using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AuseDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DegreeEnglishName",
                table: "User_Degree_Field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldPersianName",
                table: "User_Degree_Field",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DegreeEnglishName",
                table: "User_Degree_Field");

            migrationBuilder.DropColumn(
                name: "FieldPersianName",
                table: "User_Degree_Field");
        }
    }
}
