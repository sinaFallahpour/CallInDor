using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingStatusTochatMEssage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatMessageType",
                table: "ChatServiceMessages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileOrVoiceAddress",
                table: "ChatServiceMessages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatMessageType",
                table: "ChatServiceMessages");

            migrationBuilder.DropColumn(
                name: "FileOrVoiceAddress",
                table: "ChatServiceMessages");
        }
    }
}
