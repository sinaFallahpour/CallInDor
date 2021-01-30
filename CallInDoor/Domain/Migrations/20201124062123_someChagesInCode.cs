using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class someChagesInCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileConfirmType",
                table: "BaseMyService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileRejectReson",
                table: "BaseMyService",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileConfirmType",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "ProfileRejectReson",
                table: "BaseMyService");
        }
    }
}
