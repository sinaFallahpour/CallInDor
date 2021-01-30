using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class rejectinSercei : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "BaseMyService",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "BaseMyService");
        }
    }
}
