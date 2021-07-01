using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class someChnages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ReportTBL",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "ReportTBL");
        }
    }
}
