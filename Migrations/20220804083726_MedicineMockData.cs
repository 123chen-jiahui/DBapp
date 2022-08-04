using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class MedicineMockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MEDICINE",
                columns: new[] { "ID", "INDICATIONS", "INVENTORY", "MANUFACTURER", "NAME", "PRICE" },
                values: new object[] { "H19994016", "有炎症的患者", 500, "昆明贝克诺顿制药有限公司", "阿莫西林克拉维酸钾片", 20.00m });

            migrationBuilder.InsertData(
                table: "MEDICINE",
                columns: new[] { "ID", "INDICATIONS", "INVENTORY", "MANUFACTURER", "NAME", "PRICE" },
                values: new object[] { "Z20040063", "用于治疗流行性感冒属热毒袭肺症", 200, "石家庄以岭药业股份有限公司", "连花清瘟胶囊", 15.00m });

            migrationBuilder.InsertData(
                table: "MEDICINE",
                columns: new[] { "ID", "INDICATIONS", "INVENTORY", "MANUFACTURER", "NAME", "PRICE" },
                values: new object[] { "Z50020615", "用于外感风热所致的咳嗽", 350, "太极集团重庆涪陵制药厂有限公司", "急支糖浆", 25.00m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MEDICINE",
                keyColumn: "ID",
                keyValue: "H19994016");

            migrationBuilder.DeleteData(
                table: "MEDICINE",
                keyColumn: "ID",
                keyValue: "Z20040063");

            migrationBuilder.DeleteData(
                table: "MEDICINE",
                keyColumn: "ID",
                keyValue: "Z50020615");
        }
    }
}
