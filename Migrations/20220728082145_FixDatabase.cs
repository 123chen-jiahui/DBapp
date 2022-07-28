using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class FixDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_NAME",
                table: "STAFF");

            migrationBuilder.DropForeignKey(
                name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_NAME",
                table: "WARDS");

            migrationBuilder.DropIndex(
                name: "IX_WARDS_DEPARTMENT_NAME",
                table: "WARDS");

            migrationBuilder.DropIndex(
                name: "IX_STAFF_DEPARTMENT_NAME",
                table: "STAFF");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DEPARTMENTS",
                table: "DEPARTMENTS");

            migrationBuilder.DropColumn(
                name: "DEPARTMENT_NAME",
                table: "WARDS");

            migrationBuilder.DropColumn(
                name: "DEPARTMENT_NAME",
                table: "STAFF");

            migrationBuilder.AddColumn<int>(
                name: "DEPARTMENT_ID",
                table: "WARDS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DEPARTMENT_ID",
                table: "STAFF",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "DEPARTMENTS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0)
                .Annotation("Oracle:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DEPARTMENTS",
                table: "DEPARTMENTS",
                column: "ID");

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
                name: "IX_WARDS_DEPARTMENT_ID",
                table: "WARDS",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DEPARTMENT_ID",
                table: "STAFF",
                column: "DEPARTMENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_ID",
                table: "STAFF",
                column: "DEPARTMENT_ID",
                principalTable: "DEPARTMENTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_ID",
                table: "WARDS",
                column: "DEPARTMENT_ID",
                principalTable: "DEPARTMENTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_ID",
                table: "STAFF");

            migrationBuilder.DropForeignKey(
                name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_ID",
                table: "WARDS");

            migrationBuilder.DropIndex(
                name: "IX_WARDS_DEPARTMENT_ID",
                table: "WARDS");

            migrationBuilder.DropIndex(
                name: "IX_STAFF_DEPARTMENT_ID",
                table: "STAFF");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DEPARTMENTS",
                table: "DEPARTMENTS");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "ID",
                keyColumnType: "NUMBER(10)",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "DEPARTMENT_ID",
                table: "WARDS");

            migrationBuilder.DropColumn(
                name: "DEPARTMENT_ID",
                table: "STAFF");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "DEPARTMENTS");

            migrationBuilder.AddColumn<string>(
                name: "DEPARTMENT_NAME",
                table: "WARDS",
                type: "NVARCHAR2(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DEPARTMENT_NAME",
                table: "STAFF",
                type: "NVARCHAR2(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DEPARTMENTS",
                table: "DEPARTMENTS",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DEPARTMENT_NAME",
                table: "WARDS",
                column: "DEPARTMENT_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DEPARTMENT_NAME",
                table: "STAFF",
                column: "DEPARTMENT_NAME");

            migrationBuilder.AddForeignKey(
                name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_NAME",
                table: "STAFF",
                column: "DEPARTMENT_NAME",
                principalTable: "DEPARTMENTS",
                principalColumn: "NAME",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_NAME",
                table: "WARDS",
                column: "DEPARTMENT_NAME",
                principalTable: "DEPARTMENTS",
                principalColumn: "NAME",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
