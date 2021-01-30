using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addExpireTimeToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired_PeriodedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ChatForPeriodedOrSessionServiceMessagesTBL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    ChatMessageType = table.Column<int>(nullable: false),
                    FileOrVoiceAddress = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(maxLength: 40, nullable: true),
                    ClientUserName = table.Column<string>(maxLength: 40, nullable: true),
                    IsProviderSend = table.Column<bool>(nullable: false),
                    SendetMesageType = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ServiceRequestId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_ChatForPeriodedOrSessionServiceMessagesTBL_ServiceRequestId",
                table: "ChatForPeriodedOrSessionServiceMessagesTBL",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatForPeriodedOrSessionServiceMessagesTBL");

            migrationBuilder.DropColumn(
                name: "IsExpired_PeriodedChatVoice",
                table: "ServiceRequest");
        }
    }
}
