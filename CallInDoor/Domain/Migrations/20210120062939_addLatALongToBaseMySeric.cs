using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addLatALongToBaseMySeric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User-TopTenPackage_Transaction_TransactionTBLId",
                table: "User-TopTenPackage");

            migrationBuilder.DropIndex(
                name: "IX_User-TopTenPackage_TransactionTBLId",
                table: "User-TopTenPackage");

            migrationBuilder.DropColumn(
                name: "TransactionTBLId",
                table: "User-TopTenPackage");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeWithDetails",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "BaseMyService",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "BaseMyService");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTBLId",
                table: "User-TopTenPackage",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeWithDetails",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User-TopTenPackage_TransactionTBLId",
                table: "User-TopTenPackage",
                column: "TransactionTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_User-TopTenPackage_Transaction_TransactionTBLId",
                table: "User-TopTenPackage",
                column: "TransactionTBLId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
