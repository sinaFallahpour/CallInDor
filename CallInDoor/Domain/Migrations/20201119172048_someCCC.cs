using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class someCCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarCount",
                table: "BaseMyService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Under3StarCount",
                table: "BaseMyService",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarCount",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "Under3StarCount",
                table: "BaseMyService");
        }
    }
}
