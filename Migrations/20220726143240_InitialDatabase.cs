using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_PATIENT_ID",
                startValue: 1000000L);

            migrationBuilder.CreateTable(
                name: "DEPARTMENTS",
                columns: table => new
                {
                    NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENTS", x => x.NAME);
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GLOBAL_ID = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    WARD_ID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STAFF",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    GLOBAL_ID = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    ROLE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ADDRESS = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    DEPARTMENT_NAME = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_NAME",
                        column: x => x.DEPARTMENT_NAME,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WARDS",
                columns: table => new
                {
                    WARD_ID = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: false),
                    DEPARTMENT_NAME = table.Column<string>(type: "NVARCHAR2(20)", nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TYPE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STARTNUM = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ENDNUM = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WARDS", x => x.WARD_ID);
                    table.ForeignKey(
                        name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_NAME",
                        column: x => x.DEPARTMENT_NAME,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DEPARTMENT_NAME",
                table: "STAFF",
                column: "DEPARTMENT_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DEPARTMENT_NAME",
                table: "WARDS",
                column: "DEPARTMENT_NAME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PATIENTS");

            migrationBuilder.DropTable(
                name: "STAFF");

            migrationBuilder.DropTable(
                name: "WARDS");

            migrationBuilder.DropTable(
                name: "DEPARTMENTS");

            migrationBuilder.DropSequence(
                name: "SEQ_PATIENT_ID");
        }
    }
}
