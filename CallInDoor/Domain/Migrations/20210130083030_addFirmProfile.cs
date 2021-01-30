using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addFirmProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirmProfile",
                columns: table => new
                {
                    AppUserId = table.Column<string>(nullable: false),
                    FirmName = table.Column<string>(maxLength: 240, nullable: true),
                    FirmManagerName = table.Column<string>(maxLength: 240, nullable: true),
                    FirmLogo = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(maxLength: 200, nullable: true),
                    CodePosti = table.Column<string>(maxLength: 200, nullable: true),
                    FirmAddress = table.Column<string>(nullable: true),
                    FirmCountry = table.Column<string>(maxLength: 200, nullable: true),
                    FirmState = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmProfile", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_FirmProfile_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirmProfile");
        }
    }
}
