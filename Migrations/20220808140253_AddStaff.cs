using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "STAFF",
                columns: new[] { "ID", "ADDRESS", "BIRTHDAY", "DEPARTMENT_ID", "GENDER", "GLOBAL_ID", "NAME", "PASSWORD", "PHONE", "ROLE" },
                values: new object[,]
                {
                    { 2000021, "陕西省咸阳市永寿县", new DateTime(1988, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "665850236429837525", "向紫槐", "RW7fGxEV34d", "93993366799", 0 },
                    { 2000022, "湖北省十堰市勋县", new DateTime(1987, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "304894851618147779", "罗亚梅", "adhk7__dKX", "91167597036", 1 },
                    { 2000023, "江苏省连云港市赣榆县", new DateTime(1986, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, "986261509125943643", "陆田然", "(7vpTeu!b", "94341610852", 2 },
                    { 2000024, "湖北省黄冈市罗田县", new DateTime(1981, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, "962041149765665058", "邹萧玉", "w*^RviPyFC", "66558976140", 1 },
                    { 2000025, "陕西省延安市延长县", new DateTime(1977, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0, "333867359137450567", "姚密如", "fXjpNbQU", "26006740883", 2 },
                    { 2000026, "四川省乐山市犍为县", new DateTime(1989, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "858384696885017237", "双娴淑", "erCnaFOM%p", "91711269117", 1 },
                    { 2000027, "陕西省汉中市留坝县", new DateTime(1978, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 0, "481091333354292218", "顾佩兰", "c9XISFcIVhvW", "11119062912", 1 },
                    { 2000028, "山东省济宁市金乡县", new DateTime(1978, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1, "714094264311323885", "武灵枫", "zYbL1q~sR", "75857654588", 2 },
                    { 2000029, "湖南省长沙市宁乡县", new DateTime(1985, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 0, "254709473335247146", "蒲依心", "STI3x~jOQW", "64642789641", 0 },
                    { 2000030, "山东省临沂市平邑县", new DateTime(1988, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, "888034902337818811", "印香卉", "6nH4n(MQr", "86209733191", 1 },
                    { 2000031, "河北省邢台市柏乡县", new DateTime(1977, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "380234543475013804", "王紫菱", "e_NkkZ9xe^sk", "56644803281", 1 },
                    { 2000032, "江苏省淮阴市邻水县", new DateTime(1977, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "931807544686001441", "万忆山", "84H0OLa%BQ2!", "25428049805", 1 },
                    { 2000033, "安徽省安庆市岳西县", new DateTime(1979, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, "679203722177881099", "邹晶辉", "v_yHFYIjn3", "26772684495", 1 },
                    { 2000034, "浙江省杭州市淳安县", new DateTime(1968, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, "715974750548300271", "祖琴芬", "Oa&r(cWm", "45363277674", 1 },
                    { 2000035, "贵州省贵阳市修文县", new DateTime(1958, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0, "215564788365062520", "乌暄莹", "w8QKacFdtfY3", "16246675485", 2 },
                    { 2000036, "广东省韶关市翁源县", new DateTime(1982, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "225994230321642894", "邹南春", "vSw1M0ICnE~8u", "80527942982", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000021);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000022);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000023);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000024);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000025);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000026);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000027);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000028);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000029);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000030);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000031);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000032);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000033);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000034);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000035);

            migrationBuilder.DeleteData(
                table: "STAFF",
                keyColumn: "ID",
                keyValue: 2000036);
        }
    }
}
