using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sdsdwqd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ProductMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductMessages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMessages_UserId",
                table: "ProductMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMessages_AspNetUsers_UserId",
                table: "ProductMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMessages_AspNetUsers_UserId",
                table: "ProductMessages");

            migrationBuilder.DropIndex(
                name: "IX_ProductMessages_UserId",
                table: "ProductMessages");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "ProductMessages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductMessages");
        }
    }
}
