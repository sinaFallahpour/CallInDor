using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class createrelservicbaseSeervice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PackageType",
                table: "MyChatService",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceId",
                table: "BaseMyService",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMyService_Service_ServiceId",
                table: "BaseMyService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_Service_ServiceId",
                table: "BaseMyService");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_ServiceId",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "BaseMyService");

            migrationBuilder.AlterColumn<int>(
                name: "PackageType",
                table: "MyChatService",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
