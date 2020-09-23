using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class newsas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnglisTags",
                table: "ServiceTags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PersianTagName",
                table: "ServiceTags",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnglisTags",
                table: "ServiceTags");

            migrationBuilder.DropColumn(
                name: "PersianTagName",
                table: "ServiceTags");
        }
    }
}
