using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addConnectionID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatNotificationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_PriceForNativeCustomer_PriceForNonNativeCustomer",
                table: "MyChatService",
                columns: new[] { "PriceForNativeCustomer", "PriceForNonNativeCustomer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MyChatService_PriceForNativeCustomer_PriceForNonNativeCustomer",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "ChatNotificationId",
                table: "AspNetUsers");
        }
    }
}
