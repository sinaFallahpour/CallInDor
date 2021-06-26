using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class nigChnagesInDqatbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MyChatService_PriceForNativeCustomer_PriceForNonNativeCustomer",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "AcceptedMinPriceForNative",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "AcceptedMinPriceForNonNative",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "MinPriceForService",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MyServiceService");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MyCourseService");

            migrationBuilder.DropColumn(
                name: "PriceForNativeCustomer",
                table: "MyChatService");

            migrationBuilder.DropColumn(
                name: "PriceForNonNativeCustomer",
                table: "MyChatService");

            migrationBuilder.AddColumn<double>(
                name: "AcceptedMinPrice",
                table: "Service",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "BaseMyService",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RejectServiceCount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedMinPrice",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BaseMyService");

            migrationBuilder.DropColumn(
                name: "RejectServiceCount",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "AcceptedMinPriceForNative",
                table: "Service",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AcceptedMinPriceForNonNative",
                table: "Service",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinPriceForService",
                table: "Service",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "MyServiceService",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "MyCourseService",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNativeCustomer",
                table: "MyChatService",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceForNonNativeCustomer",
                table: "MyChatService",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_PriceForNativeCustomer_PriceForNonNativeCustomer",
                table: "MyChatService",
                columns: new[] { "PriceForNativeCustomer", "PriceForNonNativeCustomer" });
        }
    }
}
