using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addingEditableProfileConfirmProfileIscompanytoAppUse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Field");

            migrationBuilder.DropColumn(
                name: "PersianTitle",
                table: "Field");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Field",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompany",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditableProfile",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProfileConfirmType",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoAddress",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Field_UserId",
                table: "Field",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_AspNetUsers_UserId",
                table: "Field",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_AspNetUsers_UserId",
                table: "Field");

            migrationBuilder.DropIndex(
                name: "IX_Field_UserId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "IsCompany",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsEditableProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileConfirmType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VideoAddress",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PersianTitle",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User_Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Field_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Field_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Field_FieldId",
                table: "User_Field",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Field_UserId",
                table: "User_Field",
                column: "UserId");
        }
    }
}
