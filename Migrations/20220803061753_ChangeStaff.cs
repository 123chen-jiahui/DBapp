using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ChangeStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_PATIENT_ID",
                startValue: 1000000L);

            migrationBuilder.CreateSequence(
                name: "SEQ_STAFF_ID",
                startValue: 2000000L);

            migrationBuilder.CreateTable(
                name: "DEPARTMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENTS", x => x.ID);
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
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GLOBAL_ID = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    ROLE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ADDRESS = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_ID",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WARDS",
                columns: table => new
                {
                    WARD_ID = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: false),
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TYPE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STARTNUM = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ENDNUM = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WARDS", x => x.WARD_ID);
                    table.ForeignKey(
                        name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_ID",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRATIONS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FEE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
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

            migrationBuilder.InsertData(
                table: "DEPARTMENTS",
                columns: new[] { "ID", "BUILDING", "NAME", "PHONE" },
                values: new object[,]
                {
                    { 1, "1号楼", "内科", "11111111111" },
                    { 2, "1号楼", "儿科", "22222222222" },
                    { 3, "1号楼", "皮肤科", "33333333333" },
                    { 4, "1号楼", "急诊科", "44444444444" },
                    { 5, "2号楼", "神经科", "55555555555" },
                    { 6, "2号楼", "中医科", "66666666666" },
                    { 7, "2号楼", "外科", "77777777777" },
                    { 8, "2号楼", "眼科", "88888888888" },
                    { 9, "3号楼", "口腔科", "99999999999" },
                    { 10, "3号楼", "妇科", "00000000000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_PATIENT_ID",
                table: "REGISTRATIONS",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_STAFF_ID",
                table: "REGISTRATIONS",
                column: "STAFF_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DEPARTMENT_ID",
                table: "STAFF",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DEPARTMENT_ID",
                table: "WARDS",
                column: "DEPARTMENT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGISTRATIONS");

            migrationBuilder.DropTable(
                name: "WARDS");

            migrationBuilder.DropTable(
                name: "PATIENTS");

            migrationBuilder.DropTable(
                name: "STAFF");

            migrationBuilder.DropTable(
                name: "DEPARTMENTS");

            migrationBuilder.DropSequence(
                name: "SEQ_PATIENT_ID");

            migrationBuilder.DropSequence(
                name: "SEQ_STAFF_ID");
        }
    }
}
