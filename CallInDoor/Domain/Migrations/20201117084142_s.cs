using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCertificate_ServidceTypeRequiredCertificates_RequiredCertificatesTBLId",
                table: "ProfileCertificate");

            migrationBuilder.DropIndex(
                name: "IX_ProfileCertificate_RequiredCertificatesTBLId",
                table: "ProfileCertificate");

            migrationBuilder.DropColumn(
                name: "RequiredCertificatesTBLId",
                table: "ProfileCertificate");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCertificate_RequiredCertificatesId",
                table: "ProfileCertificate",
                column: "RequiredCertificatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileCertificate_ServidceTypeRequiredCertificates_RequiredCertificatesId",
                table: "ProfileCertificate",
                column: "RequiredCertificatesId",
                principalTable: "ServidceTypeRequiredCertificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCertificate_ServidceTypeRequiredCertificates_RequiredCertificatesId",
                table: "ProfileCertificate");

            migrationBuilder.DropIndex(
                name: "IX_ProfileCertificate_RequiredCertificatesId",
                table: "ProfileCertificate");

            migrationBuilder.AddColumn<int>(
                name: "RequiredCertificatesTBLId",
                table: "ProfileCertificate",
                type: "int",
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
    }
}
