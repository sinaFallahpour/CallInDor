using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class BaseMyService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BaseMyService");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmByAdmin",
                table: "MyCourseTopics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BaseMyService",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmByAdmin",
                table: "MyCourseTopics");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BaseMyService");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BaseMyService",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
