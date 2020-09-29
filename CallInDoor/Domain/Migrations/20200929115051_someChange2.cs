using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class someChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BaseMyService",
                type: "nvarchar(450)",
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
    }
}
