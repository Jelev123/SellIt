using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class asdsd21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForAproveds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForAproveds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForAproveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForAproveds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForAproveds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForAproveds_ProductId",
                table: "ForAproveds",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ForAproveds_UserId",
                table: "ForAproveds",
                column: "UserId");
        }
    }
}
