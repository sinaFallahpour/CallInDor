using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddMesageDForChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendetMesageType",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ServiceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "DurationSecond_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BuyiedPackageId",
                table: "ServiceRequest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_BuyiedPackageId",
                table: "ServiceRequest",
                column: "BuyiedPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_BuyiedPackage_BuyiedPackageId",
                table: "ServiceRequest",
                column: "BuyiedPackageId",
                principalTable: "BuyiedPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_BuyiedPackage_BuyiedPackageId",
                table: "ServiceRequest");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequest_BuyiedPackageId",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "BuyiedPackageId",
                table: "ServiceRequest");

            migrationBuilder.AlterColumn<int>(
                name: "DurationSecond_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SendetMesageType",
                table: "ServiceRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "ServiceRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
