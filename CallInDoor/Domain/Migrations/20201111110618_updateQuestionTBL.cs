using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class updateQuestionTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnglishText",
                table: "QuestionPull",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishText",
                table: "Answer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishText",
                table: "QuestionPull");

            migrationBuilder.DropColumn(
                name: "EnglishText",
                table: "Answer");
        }
    }
}
