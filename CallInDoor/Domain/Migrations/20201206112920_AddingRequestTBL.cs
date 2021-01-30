using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddingRequestTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienUserName = table.Column<string>(nullable: true),
                    ProvideUserName = table.Column<string>(nullable: true),
                    PackageType = table.Column<int>(nullable: false),
                    ServiceType = table.Column<int>(nullable: false),
                    ServiceRequestStatus = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    BaseServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_BaseMyService_BaseServiceId",
                        column: x => x.BaseServiceId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatServiceMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(maxLength: 40, nullable: true),
                    ClientUserName = table.Column<string>(maxLength: 40, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ServiceRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatServiceMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatServiceMessages_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatServiceMessages_ServiceRequestId",
                table: "ChatServiceMessages",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_BaseServiceId",
                table: "ServiceRequest",
                column: "BaseServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatServiceMessages");

            migrationBuilder.DropTable(
                name: "ServiceRequest");
        }
    }
}
