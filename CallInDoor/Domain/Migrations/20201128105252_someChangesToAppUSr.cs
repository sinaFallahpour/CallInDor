using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class someChangesToAppUSr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarCount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarCount",
                table: "AspNetUsers");
        }
    }
}
