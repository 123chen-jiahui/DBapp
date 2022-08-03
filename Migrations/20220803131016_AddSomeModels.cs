using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MEDICINE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(9)", maxLength: 9, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    INVENTORY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    INDICATIONS = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    ENTRY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICINE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASE_LISTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    COST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASE_LISTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PURCHASE_LISTS_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROOMS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ROOMTYPE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROOMS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASE_LIST_ITEMS",
                columns: table => new
                {
                    PURCHASE_LIST_ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    ITEM_ID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ITEM_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UNIVALENT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NUMBER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASE_LIST_ITEMS", x => x.PURCHASE_LIST_ID);
                    table.ForeignKey(
                        name: "FK_PURCHASE_LIST_ITEMS_PURCHASE_LISTS_PURCHASE_LIST_ID",
                        column: x => x.PURCHASE_LIST_ID,
                        principalTable: "PURCHASE_LISTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MEDICAL_EQUIPMENTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRODUCER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    START_USE_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ROOM_ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICAL_EQUIPMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MEDICAL_EQUIPMENTS_ROOMS_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "ROOMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MEDICAL_EQUIPMENTS_ROOM_ID",
                table: "MEDICAL_EQUIPMENTS",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_LISTS_STAFF_ID",
                table: "PURCHASE_LISTS",
                column: "STAFF_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MEDICAL_EQUIPMENTS");

            migrationBuilder.DropTable(
                name: "MEDICINE");

            migrationBuilder.DropTable(
                name: "PURCHASE_LIST_ITEMS");

            migrationBuilder.DropTable(
                name: "ROOMS");

            migrationBuilder.DropTable(
                name: "PURCHASE_LISTS");
        }
    }
}
