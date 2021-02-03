using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addIndexToSerialNumberOfAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CompanyServiceUser");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "NotificationTBL",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmStatus",
                table: "CompanyServiceUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "subSetUserName",
                table: "CompanyServiceUser",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SerialNumber",
                table: "AspNetUsers",
                column: "SerialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SerialNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConfirmStatus",
                table: "CompanyServiceUser");

            migrationBuilder.DropColumn(
                name: "subSetUserName",
                table: "CompanyServiceUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "NotificationTBL",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CompanyServiceUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
