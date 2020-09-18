using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class neServcei : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTypeTBL",
                table: "ServiceTypeTBL");

            migrationBuilder.RenameTable(
                name: "ServiceTypeTBL",
                newName: "ServiceTBL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTBL",
                table: "ServiceTBL",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTBL",
                table: "ServiceTBL");

            migrationBuilder.RenameTable(
                name: "ServiceTBL",
                newName: "ServiceTypeTBL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTypeTBL",
                table: "ServiceTypeTBL",
                column: "Id");
        }
    }
}
