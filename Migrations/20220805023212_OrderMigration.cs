using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class OrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "LINEITEM",
                type: "RAW(16)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STATE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATE_DATE_UTC = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TRANSACTION_METADATA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LINEITEM_OrderId",
                table: "LINEITEM",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_PATIENT_ID",
                table: "ORDERS",
                column: "PATIENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LINEITEM_ORDERS_OrderId",
                table: "LINEITEM",
                column: "OrderId",
                principalTable: "ORDERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LINEITEM_ORDERS_OrderId",
                table: "LINEITEM");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropIndex(
                name: "IX_LINEITEM_OrderId",
                table: "LINEITEM");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LINEITEM");
        }
    }
}
