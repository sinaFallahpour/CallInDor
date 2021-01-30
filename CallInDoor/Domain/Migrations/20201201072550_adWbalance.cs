using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class adWbalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BaseMyService_BaseMyServiceId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Descroption",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "BaseMyServiceId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transaction",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionConfirmedStatus",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WalletBalance",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BaseMyService_BaseMyServiceId",
                table: "Transaction",
                column: "BaseMyServiceId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Card_CardId",
                table: "Transaction",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BaseMyService_BaseMyServiceId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Card_CardId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionConfirmedStatus",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "WalletBalance",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "BaseMyServiceId",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descroption",
                table: "Transaction",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BaseMyService_BaseMyServiceId",
                table: "Transaction",
                column: "BaseMyServiceId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
