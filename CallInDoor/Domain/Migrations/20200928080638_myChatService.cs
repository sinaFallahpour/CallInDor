using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class myChatService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyChatService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(maxLength: 200, nullable: true),
                    PackageType = table.Column<int>(nullable: false),
                    BeTranslate = table.Column<bool>(nullable: false),
                    FreeMessageCount = table.Column<int>(nullable: false),
                    IsServiceReverse = table.Column<bool>(nullable: false),
                    PriceForNativeCustomer = table.Column<double>(nullable: false),
                    PriceForNonNativeCustomer = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ServiceType = table.Column<int>(nullable: false),
                    IsCheckedByAdmin = table.Column<bool>(nullable: false),
                    ConfirmedServiceType = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CatId = table.Column<int>(nullable: true),
                    SubCatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyChatService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyChatService_Category_CatId",
                        column: x => x.CatId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyChatService_Category_SubCatId",
                        column: x => x.SubCatId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyChatService_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_CatId",
                table: "MyChatService",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_SubCatId",
                table: "MyChatService",
                column: "SubCatId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_UserId",
                table: "MyChatService",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyChatService");
        }
    }
}
