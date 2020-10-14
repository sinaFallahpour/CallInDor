using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addNewFieldTOIdentityRiole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "MyServiceService");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "MyServiceService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "MyServiceService",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
