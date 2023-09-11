using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAPI.Migrations
{
    public partial class new_migration_add_junction_properties_unitandquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "RecipeIngredients",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "RecipeIngredients");
        }
    }
}
