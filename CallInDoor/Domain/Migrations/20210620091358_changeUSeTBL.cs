using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class changeUSeTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForNativeCustomer",
                table: "BaseServiceRequest");

            migrationBuilder.DropColumn(
                name: "PriceForNonNativeCustomer",
                table: "BaseServiceRequest");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CallRequestTBL",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealEndTime",
                table: "CallRequestTBL",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "CallRequestTBL",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptDate",
                table: "CallRequestTBL",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "BaseServiceRequest",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LimiteTimeOfRecieveRequest",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptDate",
                table: "CallRequestTBL");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BaseServiceRequest");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LimiteTimeOfRecieveRequest",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CallRequestTBL",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RealEndTime",
                table: "CallRequestTBL",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "CallRequestTBL",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNativeCustomer",
                table: "BaseServiceRequest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNonNativeCustomer",
                table: "BaseServiceRequest",
                type: "float",
                nullable: true);
        }
    }
}
