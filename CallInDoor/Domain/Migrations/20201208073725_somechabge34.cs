using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class somechabge34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ChatServiceMessages");

            //migrationBuilder.DropTable(
            //    name: "ServiceRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.CreateTable(
            //        name: "ServiceRequest",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            BaseServiceId = table.Column<int>(type: "int", nullable: true),
            //            ClienUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            PackageType = table.Column<int>(type: "int", nullable: false),
            //            ProvideUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ServiceRequestStatus = table.Column<int>(type: "int", nullable: false),
            //            ServiceType = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ServiceRequest", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ServiceRequest_BaseMyService_BaseServiceId",
            //                column: x => x.BaseServiceId,
            //                principalTable: "BaseMyService",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ChatServiceMessages",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ClientUserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
            //            CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            ProviderUserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
            //            ServiceRequestId = table.Column<int>(type: "int", nullable: true),
            //            Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ChatServiceMessages", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ChatServiceMessages_ServiceRequest_ServiceRequestId",
            //                column: x => x.ServiceRequestId,
            //                principalTable: "ServiceRequest",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ChatServiceMessages_ServiceRequestId",
            //        table: "ChatServiceMessages",
            //        column: "ServiceRequestId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ServiceRequest_BaseServiceId",
            //        table: "ServiceRequest",
            //        column: "BaseServiceId");
        }
    }
}
