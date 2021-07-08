using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class chnageToFree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseServiceRequest_BlockMony_BlockMonyId",
                table: "BaseServiceRequest");

            migrationBuilder.DropTable(
                name: "BlockMony");

            migrationBuilder.DropIndex(
                name: "IX_BaseServiceRequest_BlockMonyId",
                table: "BaseServiceRequest");

            migrationBuilder.DropColumn(
                name: "BlockMonyId",
                table: "BaseServiceRequest");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "CallRequestTBL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CallRequestTBL");

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "BlockMonyId",
                table: "BaseServiceRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BlockMony",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseRequestId = table.Column<int>(type: "int", nullable: true),
                    BlockMonyStatus = table.Column<int>(type: "int", nullable: false),
                    ClientUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreayteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProviderUsername = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockMony", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockMony_BaseServiceRequest_BaseRequestId",
                        column: x => x.BaseRequestId,
                        principalTable: "BaseServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseServiceRequest_BlockMonyId",
                table: "BaseServiceRequest",
                column: "BlockMonyId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockMony_BaseRequestId",
                table: "BlockMony",
                column: "BaseRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseServiceRequest_BlockMony_BlockMonyId",
                table: "BaseServiceRequest",
                column: "BlockMonyId",
                principalTable: "BlockMony",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
