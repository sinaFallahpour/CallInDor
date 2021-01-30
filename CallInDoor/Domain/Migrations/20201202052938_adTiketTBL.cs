using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class adTiketTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tiket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    TiketStatus = table.Column<int>(nullable: false),
                    UserLastUpdateDate = table.Column<DateTime>(nullable: false),
                    AdminLastUpdateDate = table.Column<DateTime>(nullable: false),
                    IsUserSendNewMessgae = table.Column<bool>(nullable: false),
                    IsAdminSendNewMessgae = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiketMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsFile = table.Column<bool>(nullable: false),
                    FileAddress = table.Column<string>(nullable: true),
                    TiketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiketMessages_Tiket_TiketId",
                        column: x => x.TiketId,
                        principalTable: "Tiket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TiketMessages_TiketId",
                table: "TiketMessages",
                column: "TiketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiketMessages");

            migrationBuilder.DropTable(
                name: "Tiket");
        }
    }
}
