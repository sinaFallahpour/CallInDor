using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addChtDicount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationSecond",
                table: "BuyiedPackage");

            migrationBuilder.AddColumn<int>(
                name: "CheckDiscountId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CheckDiscountId",
                table: "Transaction",
                column: "CheckDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_CheckDiscount_CheckDiscountId",
                table: "Transaction",
                column: "CheckDiscountId",
                principalTable: "CheckDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_CheckDiscount_CheckDiscountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CheckDiscountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CheckDiscountId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "DurationSecond",
                table: "BuyiedPackage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
