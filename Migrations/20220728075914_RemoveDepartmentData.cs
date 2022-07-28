using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RemoveDepartmentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "儿科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "妇科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "急诊科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "口腔科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "内科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "皮肤科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "神经科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "外科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "眼科");

            migrationBuilder.DeleteData(
                table: "DEPARTMENTS",
                keyColumn: "NAME",
                keyValue: "中医科");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DEPARTMENTS",
                columns: new[] { "NAME", "BUILDING", "PHONE" },
                values: new object[,]
                {
                    { "内科", "1号楼", "11111111111" },
                    { "儿科", "1号楼", "22222222222" },
                    { "皮肤科", "1号楼", "33333333333" },
                    { "急诊科", "1号楼", "44444444444" },
                    { "神经科", "2号楼", "55555555555" },
                    { "中医科", "2号楼", "66666666666" },
                    { "外科", "2号楼", "77777777777" },
                    { "眼科", "2号楼", "88888888888" },
                    { "口腔科", "3号楼", "99999999999" },
                    { "妇科", "3号楼", "00000000000" }
                });
        }
    }
}
