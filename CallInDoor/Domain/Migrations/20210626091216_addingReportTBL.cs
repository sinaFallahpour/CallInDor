using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingReportTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckDiscountId",
                table: "BaseServiceRequest",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscountUsedByUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CheckDiscountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountUsedByUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountUsedByUser_CheckDiscount_CheckDiscountId",
                        column: x => x.CheckDiscountId,
                        principalTable: "CheckDiscount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(nullable: false),
                    IsSucceed = table.Column<bool>(nullable: false),
                    InvoiceKey = table.Column<string>(nullable: true),
                    TransactionCode = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TrackingNumber = table.Column<string>(nullable: true),
                    ErrorDescription = table.Column<string>(nullable: true),
                    ErrorCode = table.Column<string>(nullable: true),
                    TransactionId = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportTBL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    BaseRequestServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTBL_BaseServiceRequest_BaseRequestServiceId",
                        column: x => x.BaseRequestServiceId,
                        principalTable: "BaseServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaymentId",
                table: "Transaction",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseServiceRequest_CheckDiscountId",
                table: "BaseServiceRequest",
                column: "CheckDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsedByUser_CheckDiscountId",
                table: "DiscountUsedByUser",
                column: "CheckDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TransactionId",
                table: "Payment",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTBL_BaseRequestServiceId",
                table: "ReportTBL",
                column: "BaseRequestServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseServiceRequest_CheckDiscount_CheckDiscountId",
                table: "BaseServiceRequest",
                column: "CheckDiscountId",
                principalTable: "CheckDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Payment_PaymentId",
                table: "Transaction",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseServiceRequest_CheckDiscount_CheckDiscountId",
                table: "BaseServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Payment_PaymentId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "DiscountUsedByUser");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ReportTBL");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PaymentId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_BaseServiceRequest_CheckDiscountId",
                table: "BaseServiceRequest");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CheckDiscountId",
                table: "BaseServiceRequest");
        }
    }
}
