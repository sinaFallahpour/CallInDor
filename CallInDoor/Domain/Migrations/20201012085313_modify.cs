using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.CreateTable(
                name: "MyCourseTopics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(maxLength: 200, nullable: true),
                    IsFreeForEveryOne = table.Column<bool>(nullable: false),
                    FileAddress = table.Column<string>(nullable: true),
                    MyCourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourseTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourseTopics_MyCourseService_MyCourseId",
                        column: x => x.MyCourseId,
                        principalTable: "MyCourseService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyCourseTopics_MyCourseId",
                table: "MyCourseTopics",
                column: "MyCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyCourseTopics");

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Compopnents = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Directives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFreeForEveryOne = table.Column<bool>(type: "bit", nullable: false),
                    Routings = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TopicFileAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_MyCourseService_CourseId",
                        column: x => x.CourseId,
                        principalTable: "MyCourseService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CourseId",
                table: "Topics",
                column: "CourseId");
        }
    }
}
