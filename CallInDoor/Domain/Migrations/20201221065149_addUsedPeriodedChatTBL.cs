using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addUsedPeriodedChatTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedPeriodedChat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaminingTime = table.Column<int>(nullable: false),
                    UsedTime = table.Column<int>(nullable: false),
                    ClientUserName = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(nullable: true),
                    ServiceRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedPeriodedChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedPeriodedChat_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedPeriodedChat_ServiceRequestId",
                table: "UsedPeriodedChat",
                column: "ServiceRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedPeriodedChat");
        }
    }
}
