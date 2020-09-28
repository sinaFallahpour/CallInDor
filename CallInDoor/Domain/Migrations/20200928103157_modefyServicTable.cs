using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modefyServicTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_AspNetUsers_UserId",
                table: "MyChatService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_UserId",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MyChatService");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MyChatService",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_AspNetUsers_UserName",
                table: "MyChatService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_UserName",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MyChatService");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MyChatService",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_UserId",
                table: "MyChatService",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_AspNetUsers_UserId",
                table: "MyChatService",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
