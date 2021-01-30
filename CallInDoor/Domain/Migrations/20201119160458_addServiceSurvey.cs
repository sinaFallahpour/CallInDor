using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addServiceSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 120, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    AnswerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSurvey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceSurvey_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceSurvey_QuestionPull_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionPull",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceSurvey_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSurvey_AnswerId",
                table: "ServiceSurvey",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSurvey_QuestionId",
                table: "ServiceSurvey",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSurvey_ServiceId",
                table: "ServiceSurvey",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceSurvey");
        }
    }
}
