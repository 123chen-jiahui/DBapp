using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RoomMockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ROOMS",
                columns: new[] { "ID", "BUILDING", "ROOMTYPE" },
                values: new object[,]
                {
                    { "101", "A楼", 2 },
                    { "102", "A楼", 2 },
                    { "103", "A楼", 2 },
                    { "201", "A楼", 2 },
                    { "202", "A楼", 2 },
                    { "203", "A楼", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROOMS",
                keyColumn: "ID",
                keyValue: "101");

            migrationBuilder.DeleteData(
                table: "ROOMS",
                keyColumn: "ID",
                keyValue: "102");

            migrationBuilder.DeleteData(
                table: "ROOMS",
                keyColumn: "ID",
                keyValue: "103");

            migrationBuilder.DeleteData(
                table: "ROOMS",
                keyColumn: "ID",
                keyValue: "201");

            migrationBuilder.DeleteData(
                table: "ROOMS",
                keyColumn: "ID",
                keyValue: "202");

            migrationBuilder.DeleteData(
                table: "ROOMS",
                keyColumn: "ID",
                keyValue: "203");
        }
    }
}
