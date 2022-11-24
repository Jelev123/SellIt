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
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyProductMessages_Products_ProductId",
                table: "ReplyProductMessages");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ReplyProductMessages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyProductMessages_Products_ProductId",
                table: "ReplyProductMessages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyProductMessages_Products_ProductId",
                table: "ReplyProductMessages");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ReplyProductMessages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyProductMessages_Products_ProductId",
                table: "ReplyProductMessages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
