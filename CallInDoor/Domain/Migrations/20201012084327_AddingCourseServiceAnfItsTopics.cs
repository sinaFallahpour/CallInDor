using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddingCourseServiceAnfItsTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MyServiceService",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MyCourseService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    TotalLenght = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    DisCountPercent = table.Column<int>(nullable: false),
                    PreviewVideoAddress = table.Column<string>(nullable: true),
                    BaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourseService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourseService_BaseMyService_BaseId",
                        column: x => x.BaseId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(maxLength: 100, nullable: true),
                    TopicFileAddress = table.Column<string>(nullable: true),
                    IsFreeForEveryOne = table.Column<bool>(nullable: false),
                    Compopnents = table.Column<string>(maxLength: 200, nullable: true),
                    Routings = table.Column<string>(maxLength: 200, nullable: true),
                    Directives = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
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
                name: "IX_MyCourseService_BaseId",
                table: "MyCourseService",
                column: "BaseId",
                unique: true,
                filter: "[BaseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CourseId",
                table: "Topics",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "MyCourseService");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MyServiceService",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
