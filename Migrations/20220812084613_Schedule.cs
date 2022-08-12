using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class Schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SCHEDULES",
                columns: table => new
                {
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIMESLOT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ROOM_ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    CAPACITY = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULES", x => new { x.STAFF_ID, x.DAY });
                    table.ForeignKey(
                        name: "FK_SCHEDULES_ROOMS_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "ROOMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SCHEDULES_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHEDULES_TIME_SLOTS_TIMESLOT_ID",
                        column: x => x.TIMESLOT_ID,
                        principalTable: "TIME_SLOTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_ROOM_ID",
                table: "SCHEDULES",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_TIMESLOT_ID",
                table: "SCHEDULES",
                column: "TIMESLOT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SCHEDULES");
        }
    }
}
