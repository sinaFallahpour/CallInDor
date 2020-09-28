using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modifiedServictbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_AspNetUsers_UserName",
                table: "MyChatService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_UserName",
                table: "MyChatService");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "MyChatService",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "MyChatService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_AppUserId",
                table: "MyChatService",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_AspNetUsers_AppUserId",
                table: "MyChatService",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_AspNetUsers_AppUserId",
                table: "MyChatService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_AppUserId",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "MyChatService");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "MyChatService",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_UserName",
                table: "MyChatService",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_AspNetUsers_UserName",
                table: "MyChatService",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
