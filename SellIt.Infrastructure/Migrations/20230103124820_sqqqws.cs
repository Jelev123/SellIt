using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sqqqws : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Products");
        }
    }
}
