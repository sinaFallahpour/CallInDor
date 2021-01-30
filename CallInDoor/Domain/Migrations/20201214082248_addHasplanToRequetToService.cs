using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addHasplanToRequetToService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationSecond_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPlan_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationSecond_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "HasPlan_PeriodedChatVoice",
                table: "ServiceRequest");
        }
    }
}
