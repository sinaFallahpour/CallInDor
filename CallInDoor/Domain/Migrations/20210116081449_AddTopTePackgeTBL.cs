using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddTopTePackgeTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopTenPackageId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TopTenPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    DayCount = table.Column<int>(nullable: true),
                    HourCount = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true),
                    TransactionTBLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopTenPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopTenPackage_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopTenPackage_Transaction_TransactionTBLId",
                        column: x => x.TransactionTBLId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TopTenPackageId",
                table: "Transaction",
                column: "TopTenPackageId",
                unique: true,
                filter: "[TopTenPackageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_TransactionTBLId",
                table: "TopTenPackage",
                column: "TransactionTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TopTenPackage_TopTenPackageId",
                table: "Transaction",
                column: "TopTenPackageId",
                principalTable: "TopTenPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TopTenPackage_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "TopTenPackage");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TopTenPackageId",
                table: "Transaction");
        }
    }
}
