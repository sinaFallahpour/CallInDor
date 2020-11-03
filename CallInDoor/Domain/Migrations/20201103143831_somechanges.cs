using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class somechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "ServidceTypeRequiredCertificates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PersianFileName",
                table: "ServidceTypeRequiredCertificates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "ServidceTypeRequiredCertificates");

            migrationBuilder.DropColumn(
                name: "PersianFileName",
                table: "ServidceTypeRequiredCertificates");
        }
    }
}
