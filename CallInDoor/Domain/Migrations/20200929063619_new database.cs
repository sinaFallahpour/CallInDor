using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class newdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedServiceType",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "IsCheckedByAdmin",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MyChatService");

            migrationBuilder.CreateTable(
                name: "BaseMyService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    IsCheckedByAdmin = table.Column<bool>(nullable: false),
                    ConfirmedServiceType = table.Column<int>(nullable: false),
                    ServiceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMyService", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseMyService");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmedServiceType",
                table: "MyChatService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "MyChatService",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedByAdmin",
                table: "MyChatService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "MyChatService",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "MyChatService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MyChatService",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
