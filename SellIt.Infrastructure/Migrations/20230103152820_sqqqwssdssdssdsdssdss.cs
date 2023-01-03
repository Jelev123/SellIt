using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sqqqwssdssdssdsdssdss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Adresses_ProductAdressId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductAdressId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductAdressId1",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductAdressId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductAdressId",
                table: "Products",
                column: "ProductAdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Adresses_ProductAdressId",
                table: "Products",
                column: "ProductAdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Adresses_ProductAdressId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductAdressId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductAdressId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductAdressId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductAdressId1",
                table: "Products",
                column: "ProductAdressId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Adresses_ProductAdressId1",
                table: "Products",
                column: "ProductAdressId1",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
