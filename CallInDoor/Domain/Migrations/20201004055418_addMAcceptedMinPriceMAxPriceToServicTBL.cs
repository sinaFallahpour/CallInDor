using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addMAcceptedMinPriceMAxPriceToServicTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AcceptedMinPriceForNative",
                table: "Service",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AcceptedMinPriceForNonNative",
                table: "Service",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedMinPriceForNative",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "AcceptedMinPriceForNonNative",
                table: "Service");
        }
    }
}
