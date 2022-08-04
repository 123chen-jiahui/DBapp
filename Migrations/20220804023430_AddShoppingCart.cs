using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_LINEITEM_ID");

            migrationBuilder.CreateTable(
                name: "SHOPPINGCART",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHOPPINGCART", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SHOPPINGCART_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LINEITEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MEDICINE_ID = table.Column<string>(type: "NVARCHAR2(9)", nullable: false),
                    SHOPPINGCART_ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LINEITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LINEITEM_MEDICINE_MEDICINE_ID",
                        column: x => x.MEDICINE_ID,
                        principalTable: "MEDICINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LINEITEM_SHOPPINGCART_SHOPPINGCART_ID",
                        column: x => x.SHOPPINGCART_ID,
                        principalTable: "SHOPPINGCART",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LINEITEM_MEDICINE_ID",
                table: "LINEITEM",
                column: "MEDICINE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LINEITEM_SHOPPINGCART_ID",
                table: "LINEITEM",
                column: "SHOPPINGCART_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SHOPPINGCART_PATIENT_ID",
                table: "SHOPPINGCART",
                column: "PATIENT_ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LINEITEM");

            migrationBuilder.DropTable(
                name: "SHOPPINGCART");

            migrationBuilder.DropSequence(
                name: "SEQ_LINEITEM_ID");
        }
    }
}
