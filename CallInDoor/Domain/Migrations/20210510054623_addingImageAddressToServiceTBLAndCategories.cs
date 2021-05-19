using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingImageAddressToServiceTBLAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_BaseMyService_BaseId",
                table: "MyChatService");

            migrationBuilder.DropForeignKey(
                name: "FK_MyServiceService_BaseMyService_BaseId",
                table: "MyServiceService");

            migrationBuilder.AddColumn<string>(
                name: "ImageAddress",
                table: "Service",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageAddress",
                table: "Category",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_BaseMyService_BaseId",
                table: "MyChatService",
                column: "BaseId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyServiceService_BaseMyService_BaseId",
                table: "MyServiceService",
                column: "BaseId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_BaseMyService_BaseId",
                table: "MyChatService");

            migrationBuilder.DropForeignKey(
                name: "FK_MyServiceService_BaseMyService_BaseId",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "ImageAddress",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ImageAddress",
                table: "Category");

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_BaseMyService_BaseId",
                table: "MyChatService",
                column: "BaseId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyServiceService_BaseMyService_BaseId",
                table: "MyServiceService",
                column: "BaseId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
