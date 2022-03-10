using Microsoft.EntityFrameworkCore.Migrations;

namespace WoltDataAccess.Migrations
{
    public partial class CreateIsNewColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Discount",
                table: "Restaurants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiscountPercent",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Restaurants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Discount",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiscountPercent",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Products");
        }
    }
}
