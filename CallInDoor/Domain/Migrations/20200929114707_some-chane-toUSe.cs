using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class somechanetoUSe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_AppUserId",
                table: "BaseMyService",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMyService_AspNetUsers_AppUserId",
                table: "BaseMyService",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_AspNetUsers_AppUserId",
                table: "BaseMyService");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_AppUserId",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BaseMyService");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "MyChatService",
                type: "nvarchar(450)",
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
    }
}
