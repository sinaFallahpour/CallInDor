using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddServiceService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyServiceService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 600, nullable: true),
                    BeTranslate = table.Column<bool>(nullable: false),
                    FileNeeded = table.Column<bool>(nullable: false),
                    FilrDescription = table.Column<string>(maxLength: 600, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    WorkDeliveryTimeEstimation = table.Column<string>(maxLength: 600, nullable: true),
                    HowWorkConducts = table.Column<string>(maxLength: 600, nullable: true),
                    DeliveryItems = table.Column<string>(maxLength: 600, nullable: true),
                    Tags = table.Column<string>(maxLength: 600, nullable: true),
                    CatId = table.Column<int>(nullable: true),
                    SubCatId = table.Column<int>(nullable: true),
                    BaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyServiceService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyServiceService_BaseMyService_BaseId",
                        column: x => x.BaseId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyServiceService_Category_CatId",
                        column: x => x.CatId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyServiceService_Category_SubCatId",
                        column: x => x.SubCatId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_BaseId",
                table: "MyServiceService",
                column: "BaseId",
                unique: true,
                filter: "[BaseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_CatId",
                table: "MyServiceService",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_SubCatId",
                table: "MyServiceService",
                column: "SubCatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyServiceService");
        }
    }
}
