using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingAlotChnages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRequest_BuyiedPackage_BuyiedPackageId",
                table: "ChatRequest");

            migrationBuilder.DropIndex(
                name: "IX_ChatRequest_BuyiedPackageId",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "BuyiedPackageId",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "HasPlan_LimitedChatVoice",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "PackageType",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "PriceForNativeCustomer",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "PriceForNonNativeCustomer",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "WhenTheRequestShouldBeAnswered",
                table: "ChatRequest");

            migrationBuilder.DropColumn(
                name: "BuyiedPackageStatus",
                table: "BuyiedPackage");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptDate",
                table: "ChatRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptDate",
                table: "ChatRequest");

            migrationBuilder.AddColumn<int>(
                name: "BuyiedPackageId",
                table: "ChatRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPlan_LimitedChatVoice",
                table: "ChatRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PackageType",
                table: "ChatRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNativeCustomer",
                table: "ChatRequest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNonNativeCustomer",
                table: "ChatRequest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WhenTheRequestShouldBeAnswered",
                table: "ChatRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BuyiedPackageStatus",
                table: "BuyiedPackage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRequest_BuyiedPackageId",
                table: "ChatRequest",
                column: "BuyiedPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRequest_BuyiedPackage_BuyiedPackageId",
                table: "ChatRequest",
                column: "BuyiedPackageId",
                principalTable: "BuyiedPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
