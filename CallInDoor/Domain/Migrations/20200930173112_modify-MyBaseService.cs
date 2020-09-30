using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modifyMyBaseService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_Category_CatId",
                table: "MyChatService");

            migrationBuilder.DropForeignKey(
                name: "FK_MyChatService_Category_SubCatId",
                table: "MyChatService");

            migrationBuilder.DropForeignKey(
                name: "FK_MyServiceService_Category_CatId",
                table: "MyServiceService");

            migrationBuilder.DropForeignKey(
                name: "FK_MyServiceService_Category_SubCatId",
                table: "MyServiceService");

            migrationBuilder.DropIndex(
                name: "IX_MyServiceService_CatId",
                table: "MyServiceService");

            migrationBuilder.DropIndex(
                name: "IX_MyServiceService_SubCatId",
                table: "MyServiceService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_CatId",
                table: "MyChatService");

            migrationBuilder.DropIndex(
                name: "IX_MyChatService_SubCatId",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "SubCatId",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "SubCatId",
                table: "MyChatService");

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCatId",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_CatId",
                table: "BaseMyService",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_SubCatId",
                table: "BaseMyService",
                column: "SubCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMyService_Category_CatId",
                table: "BaseMyService",
                column: "CatId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMyService_Category_SubCatId",
                table: "BaseMyService",
                column: "SubCatId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_Category_CatId",
                table: "BaseMyService");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_Category_SubCatId",
                table: "BaseMyService");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_CatId",
                table: "BaseMyService");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_SubCatId",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "SubCatId",
                table: "BaseMyService");

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "MyServiceService",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCatId",
                table: "MyServiceService",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "MyChatService",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCatId",
                table: "MyChatService",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_CatId",
                table: "MyServiceService",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_SubCatId",
                table: "MyServiceService",
                column: "SubCatId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_CatId",
                table: "MyChatService",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_SubCatId",
                table: "MyChatService",
                column: "SubCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_Category_CatId",
                table: "MyChatService",
                column: "CatId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyChatService_Category_SubCatId",
                table: "MyChatService",
                column: "SubCatId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyServiceService_Category_CatId",
                table: "MyServiceService",
                column: "CatId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyServiceService_Category_SubCatId",
                table: "MyServiceService",
                column: "SubCatId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
