using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingpackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyiedPackage_BaseMyService_BaseMyServiceId",
                table: "BuyiedPackage");

            migrationBuilder.DropIndex(
                name: "IX_BuyiedPackage_BaseMyServiceId",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "IsExpired_LimitedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "BaseMyServiceId",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "ClientUserName",
                table: "BuyiedPackage");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTime_LimitedChatVoice",
                table: "ServiceRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CheckDiscountId",
                table: "BuyiedPackage",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "BuyiedPackage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTime",
                table: "BuyiedPackage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MessgaeCount",
                table: "BuyiedPackage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BuyiedPackage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuyiedPackage_CheckDiscountId",
                table: "BuyiedPackage",
                column: "CheckDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyiedPackage_CheckDiscount_CheckDiscountId",
                table: "BuyiedPackage",
                column: "CheckDiscountId",
                principalTable: "CheckDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyiedPackage_CheckDiscount_CheckDiscountId",
                table: "BuyiedPackage");

            migrationBuilder.DropIndex(
                name: "IX_BuyiedPackage_CheckDiscountId",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "ExpireTime_LimitedChatVoice",
                table: "ServiceRequest");

            migrationBuilder.DropColumn(
                name: "CheckDiscountId",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "ExpireTime",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "MessgaeCount",
                table: "BuyiedPackage");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BuyiedPackage");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired_LimitedChatVoice",
                table: "ServiceRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BaseMyServiceId",
                table: "BuyiedPackage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientUserName",
                table: "BuyiedPackage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuyiedPackage_BaseMyServiceId",
                table: "BuyiedPackage",
                column: "BaseMyServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyiedPackage_BaseMyService_BaseMyServiceId",
                table: "BuyiedPackage",
                column: "BaseMyServiceId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
