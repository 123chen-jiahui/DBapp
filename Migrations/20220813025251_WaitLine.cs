using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class WaitLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WAITLINES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ORDER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAITLINES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WAITLINES_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WAITLINES_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WAITLINES_PATIENT_ID",
                table: "WAITLINES",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WAITLINES_STAFF_ID",
                table: "WAITLINES",
                column: "STAFF_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WAITLINES");
        }
    }
}
