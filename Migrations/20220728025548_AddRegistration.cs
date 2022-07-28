using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "REGISTRATIONS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FEE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STAFF_ID = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REGISTRATIONS_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATIONS_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_PATIENT_ID",
                table: "REGISTRATIONS",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_STAFF_ID",
                table: "REGISTRATIONS",
                column: "STAFF_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGISTRATIONS");
        }
    }
}
