using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class somecc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Service_ServiceId",
                table: "ServiceComments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceComments_ServiceId",
                table: "ServiceComments");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceComments");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "QuestionPull");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "BaseMyServiceId",
                table: "ServiceComments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceTBLId",
                table: "ServiceComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComments_BaseMyServiceId",
                table: "ServiceComments",
                column: "BaseMyServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComments_ServiceTBLId",
                table: "ServiceComments",
                column: "ServiceTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceName",
                table: "BaseMyService",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceName_ServiceType",
                table: "BaseMyService",
                columns: new[] { "ServiceName", "ServiceType" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_BaseMyService_BaseMyServiceId",
                table: "ServiceComments",
                column: "BaseMyServiceId",
                principalTable: "BaseMyService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Service_ServiceTBLId",
                table: "ServiceComments",
                column: "ServiceTBLId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_BaseMyService_BaseMyServiceId",
                table: "ServiceComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Service_ServiceTBLId",
                table: "ServiceComments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceComments_BaseMyServiceId",
                table: "ServiceComments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceComments_ServiceTBLId",
                table: "ServiceComments");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_ServiceName",
                table: "BaseMyService");

            migrationBuilder.DropIndex(
                name: "IX_BaseMyService_ServiceName_ServiceType",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "BaseMyServiceId",
                table: "ServiceComments");

            migrationBuilder.DropColumn(
                name: "ServiceTBLId",
                table: "ServiceComments");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ServiceComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "QuestionPull",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Answer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComments_ServiceId",
                table: "ServiceComments",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Service_ServiceId",
                table: "ServiceComments",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
