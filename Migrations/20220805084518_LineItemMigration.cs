using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class LineItemMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LINEITEM_ORDERS_OrderId",
                table: "LINEITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_LINEITEM_SHOPPINGCART_SHOPPINGCART_ID",
                table: "LINEITEM");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "LINEITEM",
                newName: "ORDER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_LINEITEM_OrderId",
                table: "LINEITEM",
                newName: "IX_LINEITEM_ORDER_ID");

            migrationBuilder.AlterColumn<Guid>(
                name: "SHOPPINGCART_ID",
                table: "LINEITEM",
                type: "RAW(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "RAW(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_LINEITEM_ORDERS_ORDER_ID",
                table: "LINEITEM",
                column: "ORDER_ID",
                principalTable: "ORDERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LINEITEM_SHOPPINGCART_SHOPPINGCART_ID",
                table: "LINEITEM",
                column: "SHOPPINGCART_ID",
                principalTable: "SHOPPINGCART",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LINEITEM_ORDERS_ORDER_ID",
                table: "LINEITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_LINEITEM_SHOPPINGCART_SHOPPINGCART_ID",
                table: "LINEITEM");

            migrationBuilder.RenameColumn(
                name: "ORDER_ID",
                table: "LINEITEM",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_LINEITEM_ORDER_ID",
                table: "LINEITEM",
                newName: "IX_LINEITEM_OrderId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SHOPPINGCART_ID",
                table: "LINEITEM",
                type: "RAW(16)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "RAW(16)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LINEITEM_ORDERS_OrderId",
                table: "LINEITEM",
                column: "OrderId",
                principalTable: "ORDERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LINEITEM_SHOPPINGCART_SHOPPINGCART_ID",
                table: "LINEITEM",
                column: "SHOPPINGCART_ID",
                principalTable: "SHOPPINGCART",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
