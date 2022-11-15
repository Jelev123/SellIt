using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemoteImageUrl",
                table: "Images",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "Images",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Images",
                newName: "RemoteImageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Images",
                newName: "Extension");
        }
    }
}
