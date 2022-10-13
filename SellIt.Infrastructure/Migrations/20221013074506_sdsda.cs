using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sdsda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCounter",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ClickCounter",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
