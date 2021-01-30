using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addBuyiedPackageTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyiedPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientUserName = table.Column<string>(nullable: true),
                    DurationSecond = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: true),
                    BuyiedPackageType = table.Column<int>(nullable: false),
                    BaseMyServiceId = table.Column<int>(nullable: true),
                    ServiceRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyiedPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyiedPackage_BaseMyService_BaseMyServiceId",
                        column: x => x.BaseMyServiceId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyiedPackage_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyiedPackage_BaseMyServiceId",
                table: "BuyiedPackage",
                column: "BaseMyServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyiedPackage_ServiceRequestId",
                table: "BuyiedPackage",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyiedPackage");
        }
    }
}
