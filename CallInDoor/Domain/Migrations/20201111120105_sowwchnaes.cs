using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class sowwchnaes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_QuestionPull_QuestionPullTBLId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionPullTBLId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "QuestionPullTBLId",
                table: "Answer");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_QuestionPull_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "QuestionPull",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_QuestionPull_QuestionId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "QuestionPullTBLId",
                table: "Answer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionPullTBLId",
                table: "Answer",
                column: "QuestionPullTBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_QuestionPull_QuestionPullTBLId",
                table: "Answer",
                column: "QuestionPullTBLId",
                principalTable: "QuestionPull",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
