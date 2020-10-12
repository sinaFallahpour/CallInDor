using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingSpecialiyAndAreaToServiceService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "MyServiceService",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "MyServiceService",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "MyServiceService",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "MyServiceService",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_AreaId",
                table: "MyServiceService",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_SpecialityId",
                table: "MyServiceService",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyServiceService_Area_AreaId",
                table: "MyServiceService",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyServiceService_Speciality_SpecialityId",
                table: "MyServiceService",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyServiceService_Area_AreaId",
                table: "MyServiceService");

            migrationBuilder.DropForeignKey(
                name: "FK_MyServiceService_Speciality_SpecialityId",
                table: "MyServiceService");

            migrationBuilder.DropIndex(
                name: "IX_MyServiceService_AreaId",
                table: "MyServiceService");

            migrationBuilder.DropIndex(
                name: "IX_MyServiceService_SpecialityId",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "MyServiceService");
        }
    }
}
