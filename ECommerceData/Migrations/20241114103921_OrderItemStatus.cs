using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "orderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderItemStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemStatus", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_StatusId",
                table: "orderItems",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_OrderItemStatus_StatusId",
                table: "orderItems",
                column: "StatusId",
                principalTable: "OrderItemStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_OrderItemStatus_StatusId",
                table: "orderItems");

            migrationBuilder.DropTable(
                name: "OrderItemStatus");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_StatusId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "orderItems");
        }
    }
}
