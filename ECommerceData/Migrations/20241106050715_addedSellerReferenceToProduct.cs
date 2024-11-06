using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedSellerReferenceToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "SellerId",
                value: "f300ff54-1b2e-43f9-a700-b04ff20d6153");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "SellerId",
                value: "f300ff54-1b2e-43f9-a700-b04ff20d6153");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                column: "SellerId",
                value: "f300ff54-1b2e-43f9-a700-b04ff20d6153");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SellerId",
                table: "Product",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_SellerId",
                table: "Product",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_SellerId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_SellerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Product");
        }
    }
}
