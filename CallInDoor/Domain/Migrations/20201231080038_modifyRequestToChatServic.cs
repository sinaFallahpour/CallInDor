using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modifyRequestToChatServic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatForPeriodedOrSessionServiceMessagesTBL");

            migrationBuilder.DropTable(
                name: "UsedPeriodedChat");

            migrationBuilder.DropColumn(
                name: "DurationSecond_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "EndTime_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "HasPlan_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "IsExpired_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "IsPerodedOrsesionChat",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "ReaminingTime_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "StartTime_PeriodedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.AddColumn<int>(
                name: "AllMessageCount_LimitedChat",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPlan_LimitedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired_LimitedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLimitedChat",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UsedMessageCount_LimitedChat",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChatForLimitedServiceMessagesTBL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    ChatMessageType = table.Column<int>(nullable: false),
                    FileOrVoiceAddress = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(maxLength: 100, nullable: true),
                    ClientUserName = table.Column<string>(maxLength: 100, nullable: true),
                    IsProviderSend = table.Column<bool>(nullable: false),
                    SendetMesageType = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ServiceRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatForLimitedServiceMessagesTBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatForLimitedServiceMessagesTBL_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatForLimitedServiceMessagesTBL_ServiceRequestId",
                table: "ChatForLimitedServiceMessagesTBL",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatForLimitedServiceMessagesTBL");

            migrationBuilder.DropColumn(
                name: "AllMessageCount_LimitedChat",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "HasPlan_LimitedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "IsExpired_LimitedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "IsLimitedChat",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "UsedMessageCount_LimitedChat",
                table: "ServiceRequest");

            migrationBuilder.AddColumn<int>(
                name: "DurationSecond_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HasPlan_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPerodedOrsesionChat",
                table: "ServiceRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReaminingTime_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime_PeriodedChatVoice",
                table: "ServiceRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ChatForPeriodedOrSessionServiceMessagesTBL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatMessageType = table.Column<int>(type: "int", nullable: false),
                    ClientUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileOrVoiceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProviderSend = table.Column<bool>(type: "bit", nullable: false),
                    ProviderUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SendetMesageType = table.Column<int>(type: "int", nullable: false),
                    ServiceRequestId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatForPeriodedOrSessionServiceMessagesTBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatForPeriodedOrSessionServiceMessagesTBL_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsedPeriodedChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProviderUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceRequestId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedPeriodedChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedPeriodedChat_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatForPeriodedOrSessionServiceMessagesTBL_ServiceRequestId",
                table: "ChatForPeriodedOrSessionServiceMessagesTBL",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedPeriodedChat_ServiceRequestId",
                table: "UsedPeriodedChat",
                column: "ServiceRequestId");
        }
    }
}
