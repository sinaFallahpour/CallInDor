using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class someChngesToAppUSer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Under3StarCount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Under3StarCount",
                table: "AspNetUsers");
        }
    }
}
