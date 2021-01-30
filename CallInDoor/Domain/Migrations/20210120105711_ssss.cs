using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class ssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User-TopTenPackage_UserTopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserTopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserTopTenPackageId",
                table: "Transaction");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "BaseMyService",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_User_TopTenPackageId",
                table: "Transaction",
                column: "User_TopTenPackageId",
                unique: true,
                filter: "[User_TopTenPackageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User-TopTenPackage_User_TopTenPackageId",
                table: "Transaction",
                column: "User_TopTenPackageId",
                principalTable: "User-TopTenPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User-TopTenPackage_User_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_User_TopTenPackageId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "UserTopTenPackageId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "BaseMyService",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserTopTenPackageId",
                table: "Transaction",
                column: "UserTopTenPackageId",
                unique: true,
                filter: "[UserTopTenPackageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User-TopTenPackage_UserTopTenPackageId",
                table: "Transaction",
                column: "UserTopTenPackageId",
                principalTable: "User-TopTenPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
