using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modifingrelationServicetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService",
                column: "BaseId",
                unique: true,
                filter: "[BaseId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService",
                column: "BaseId");
        }
    }
}
