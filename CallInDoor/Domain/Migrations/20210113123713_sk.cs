using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class sk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "BuyiedPackage");

            migrationBuilder.AddColumn<int>(
                name: "BuyiedPackageStatus",
                table: "BuyiedPackage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "FinalPrice",
                table: "BuyiedPackage",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MainPrice",
                table: "BuyiedPackage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SitePercent",
                table: "BuyiedPackage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyiedPackageStatus",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "MainPrice",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "SitePercent",
                table: "BuyiedPackage");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "BuyiedPackage",
                type: "float",
                nullable: true);
        }
    }
}
