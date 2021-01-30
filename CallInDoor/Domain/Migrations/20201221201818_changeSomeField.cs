using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class changeSomeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReaminingTime",
                table: "UsedPeriodedChat");

            migrationBuilder.AddColumn<int>(
                name: "ReaminingTime_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReaminingTime_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.AddColumn<int>(
                name: "ReaminingTime",
                table: "UsedPeriodedChat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
