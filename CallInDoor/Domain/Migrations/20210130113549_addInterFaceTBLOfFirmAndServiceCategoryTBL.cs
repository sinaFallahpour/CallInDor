using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addInterFaceTBLOfFirmAndServiceCategoryTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirmServiceCategoryInterInterFace",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTBLId = table.Column<int>(nullable: true),
                    FirmProfileTBLId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmServiceCategoryInterInterFace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmServiceCategoryInterInterFace_FirmProfile_FirmProfileTBLId",
                        column: x => x.FirmProfileTBLId,
                        principalTable: "FirmProfile",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FirmServiceCategoryInterInterFace_Service_ServiceTBLId",
                        column: x => x.ServiceTBLId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirmServiceCategoryInterInterFace_FirmProfileTBLId",
                table: "FirmServiceCategoryInterInterFace",
                column: "FirmProfileTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmServiceCategoryInterInterFace_ServiceTBLId",
                table: "FirmServiceCategoryInterInterFace",
                column: "ServiceTBLId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirmServiceCategoryInterInterFace");
        }
    }
}
