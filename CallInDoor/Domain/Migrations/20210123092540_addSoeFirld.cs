using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addSoeFirld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderUserName",
                table: "ChatServiceMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderUserName",
                table: "ChatForLimitedServiceMessagesTBL",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderUserName",
                table: "ChatServiceMessages");

            migrationBuilder.DropColumn(
                name: "SenderUserName",
                table: "ChatForLimitedServiceMessagesTBL");
        }
    }
}
