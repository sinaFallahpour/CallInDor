using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingBlockMony : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockMonyId",
                table: "BaseServiceRequest",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlockMony",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientUsername = table.Column<string>(nullable: true),
                    ProviderUsername = table.Column<string>(nullable: true),
                    CreayteDate = table.Column<DateTime>(nullable: false),
                    BlockMonyStatus = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    BaseRequestId = table.Column<int>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
