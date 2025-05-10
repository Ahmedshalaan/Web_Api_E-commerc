using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_FristName",
                table: "Orders",
                newName: "shippingAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "FristName",
                table: "Address",
                newName: "FirstName");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_FirstName",
                table: "Orders",
                newName: "shippingAddress_FristName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Address",
                newName: "FristName");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
