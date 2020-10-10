using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addSpeciality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialities",
                table: "Area");

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianName = table.Column<string>(nullable: true),
                    EnglishName = table.Column<string>(nullable: true),
                    AreatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speciality_Area_AreatId",
                        column: x => x.AreatId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Speciality_AreatId",
                table: "Speciality",
                column: "AreatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "Specialities",
                table: "Area",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: true);
        }
    }
}
