using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class fixSomeBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceForNativeCustomer",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNonNativeCustomer",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishTitle",
                table: "CheckDiscount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersianTitle",
                table: "CheckDiscount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForNativeCustomer",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "PriceForNonNativeCustomer",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "EnglishTitle",
                table: "CheckDiscount");

            migrationBuilder.DropColumn(
                name: "PersianTitle",
                table: "CheckDiscount");
        }
    }
}
