using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addDisabledTobaseService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabledByCompany",
                table: "BaseMyService",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_CompanyId",
                table: "BaseMyService",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMyService_AspNetUsers_CompanyId",
                table: "BaseMyService",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_AspNetUsers_CompanyId",
                table: "BaseMyService");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_CompanyId",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "IsDisabledByCompany",
                table: "BaseMyService");
        }
    }
}
