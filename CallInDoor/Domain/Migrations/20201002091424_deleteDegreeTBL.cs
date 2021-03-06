﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class deleteDegreeTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Degree_DegreeId",
                table: "Field");

            migrationBuilder.DropTable(
                name: "User_Degree_Field");

            migrationBuilder.DropTable(
                name: "Degree");

            migrationBuilder.DropIndex(
                name: "IX_Field_DegreeId",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "DegreeType",
                table: "Field",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User_Field",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    FieldId = table.Column<int>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Field");

            migrationBuilder.DropColumn(
                name: "DegreeType",
                table: "Field");

            migrationBuilder.AddColumn<int>(
                name: "DegreeId",
                table: "Field",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Degree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degree", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Degree_Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegreeId = table.Column<int>(type: "int", nullable: true),
                    DegreeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreePersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldId = table.Column<int>(type: "int", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldPersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Degree_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Degree_Field_Degree_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Degree_Field_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Degree_Field_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_DegreeId",
                table: "Field",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Degree_Field_DegreeId",
                table: "User_Degree_Field",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Degree_Field_FieldId",
                table: "User_Degree_Field",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Degree_Field_UserId",
                table: "User_Degree_Field",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Degree_DegreeId",
                table: "Field",
                column: "DegreeId",
                principalTable: "Degree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
