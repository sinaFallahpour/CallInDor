using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modifyBAseService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCertificateTBL_Service_ServiceId",
                table: "ProfileCertificateTBL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileCertificateTBL",
                table: "ProfileCertificateTBL");

            migrationBuilder.RenameTable(
                name: "ProfileCertificateTBL",
                newName: "ProfileCertificate");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileCertificateTBL_ServiceId",
                table: "ProfileCertificate",
                newName: "IX_ProfileCertificate_ServiceId");

            migrationBuilder.AddColumn<bool>(
                name: "IsEditableService",
                table: "BaseMyService",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileCertificate",
                table: "ProfileCertificate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileCertificate_Service_ServiceId",
                table: "ProfileCertificate",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileCertificate_Service_ServiceId",
                table: "ProfileCertificate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileCertificate",
                table: "ProfileCertificate");

            migrationBuilder.DropColumn(
                name: "IsEditableService",
                table: "BaseMyService");

            migrationBuilder.RenameTable(
                name: "ProfileCertificate",
                newName: "ProfileCertificateTBL");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileCertificate_ServiceId",
                table: "ProfileCertificateTBL",
                newName: "IX_ProfileCertificateTBL_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileCertificateTBL",
                table: "ProfileCertificateTBL",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileCertificateTBL_Service_ServiceId",
                table: "ProfileCertificateTBL",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
