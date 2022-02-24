using Microsoft.EntityFrameworkCore.Migrations;

namespace WoltDataAccess.Migrations
{
    public partial class CreateControllerColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Stores",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Restaurants",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Categories",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Categories");
        }
    }
}
