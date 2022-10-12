using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sdsds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClickCounter",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCounter",
                table: "Products");
        }
    }
}
