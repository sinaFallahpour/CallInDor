using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingUserWidrawlRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWithdrawlRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    WithdrawlRequestStatus = table.Column<int>(nullable: false),
                    ResonOfReject = table.Column<string>(nullable: true),
                    UserName = table.Column<int>(nullable: false),
                    CardItId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWithdrawlRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWithdrawlRequest_Card_CardItId",
                        column: x => x.CardItId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWithdrawlRequest_CardItId",
                table: "UserWithdrawlRequest",
                column: "CardItId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWithdrawlRequest");
        }
    }
}
