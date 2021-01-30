using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addIntrfaceOfUserAdTopTen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopTenPackage_Transaction_TransactionTBLId",
                table: "TopTenPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TopTenPackage_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_TopTenPackage_TransactionTBLId",
                table: "TopTenPackage");

            migrationBuilder.DropColumn(
                name: "TopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionTBLId",
                table: "TopTenPackage");

            migrationBuilder.AddColumn<int>(
                name: "UserTopTenPackageId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User_TopTenPackageId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User-TopTenPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    TransactionTBLId = table.Column<int>(nullable: true),
                    TopTenPackageId = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User-TopTenPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User-TopTenPackage_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User-TopTenPackage_TopTenPackage_TopTenPackageId",
                        column: x => x.TopTenPackageId,
                        principalTable: "TopTenPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User-TopTenPackage_Transaction_TransactionTBLId",
                        column: x => x.TransactionTBLId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserTopTenPackageId",
                table: "Transaction",
                column: "UserTopTenPackageId",
                unique: true,
                filter: "[UserTopTenPackageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User-TopTenPackage_ServiceId",
                table: "User-TopTenPackage",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User-TopTenPackage_TopTenPackageId",
                table: "User-TopTenPackage",
                column: "TopTenPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_User-TopTenPackage_TransactionTBLId",
                table: "User-TopTenPackage",
                column: "TransactionTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User-TopTenPackage_UserTopTenPackageId",
                table: "Transaction",
                column: "UserTopTenPackageId",
                principalTable: "User-TopTenPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User-TopTenPackage_UserTopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "User-TopTenPackage");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserTopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserTopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "User_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "TopTenPackageId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTBLId",
                table: "TopTenPackage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TopTenPackageId",
                table: "Transaction",
                column: "TopTenPackageId",
                unique: true,
                filter: "[TopTenPackageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_TransactionTBLId",
                table: "TopTenPackage",
                column: "TransactionTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopTenPackage_Transaction_TransactionTBLId",
                table: "TopTenPackage",
                column: "TransactionTBLId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TopTenPackage_TopTenPackageId",
                table: "Transaction",
                column: "TopTenPackageId",
                principalTable: "TopTenPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
