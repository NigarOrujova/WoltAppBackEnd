using Microsoft.EntityFrameworkCore.Migrations;

namespace WoltDataAccess.Migrations
{
    public partial class CreateDiscountColum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Discount",
                table: "Stores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiscountPercent",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Stores",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Stores");
        }
    }
}
