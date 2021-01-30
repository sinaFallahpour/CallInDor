using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class nnnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SitePercent",
                table: "Service",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SitePercent",
                table: "Service",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
