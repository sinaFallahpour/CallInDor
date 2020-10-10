using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class editArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Area");

            migrationBuilder.AddColumn<string>(
                name: "Specialities",
                table: "Area",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialities",
                table: "Area");

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Area",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
