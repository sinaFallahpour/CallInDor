using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addstatDtadandEndDtatetoUSedPerodedChatTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "UsedPeriodedChat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "UsedPeriodedChat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "UsedPeriodedChat");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "UsedPeriodedChat");

            migrationBuilder.DropColumn(
                name: "EndTime_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "StartTime_PeriodedChatVoice",
                table: "ServiceRequest");
        }
    }
}
