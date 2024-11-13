using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "order");
        }
    }
}
