using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class bseService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseId",
                table: "MyChatService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService",
                column: "BaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_BaseMyService_BaseId",
                table: "MyChatService",
                column: "BaseId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_BaseMyService_BaseId",
                table: "MyChatService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "MyChatService");
        }
    }
}
