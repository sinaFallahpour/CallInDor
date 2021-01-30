using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class add_ISProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProviderSend",
                table: "ChatServiceMessages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProviderSend",
                table: "ChatServiceMessages");
        }
    }
}
