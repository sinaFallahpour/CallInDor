using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class somemodifyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequiredCertificatesId",
                table: "ProfileCertificate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequiredCertificatesTBLId",
                table: "ProfileCertificate",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCertificate_RequiredCertificatesTBLId",
                table: "ProfileCertificate",
                column: "RequiredCertificatesTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileCertificate_ServidceTypeRequiredCertificates_RequiredCertificatesTBLId",
                table: "ProfileCertificate",
                column: "RequiredCertificatesTBLId",
                principalTable: "ServidceTypeRequiredCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCertificate_ServidceTypeRequiredCertificates_RequiredCertificatesTBLId",
                table: "ProfileCertificate");

            migrationBuilder.DropIndex(
                name: "IX_ProfileCertificate_RequiredCertificatesTBLId",
                table: "ProfileCertificate");

            migrationBuilder.DropColumn(
                name: "RequiredCertificatesId",
                table: "ProfileCertificate");

            migrationBuilder.DropColumn(
                name: "RequiredCertificatesTBLId",
                table: "ProfileCertificate");
        }
    }
}
