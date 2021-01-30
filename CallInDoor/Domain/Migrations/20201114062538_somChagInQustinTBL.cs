using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class somChagInQustinTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "QuestionPull",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "QuestionPull",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Answer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPull_ServiceId",
                table: "QuestionPull",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPull_Service_ServiceId",
                table: "QuestionPull",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPull_Service_ServiceId",
                table: "QuestionPull");

            migrationBuilder.DropIndex(
                name: "IX_QuestionPull_ServiceId",
                table: "QuestionPull");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "QuestionPull");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "QuestionPull");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Answer");
        }
    }
}
