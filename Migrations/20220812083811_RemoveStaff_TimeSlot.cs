using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RemoveStaff_TimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STAFF_TIMESLOT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STAFF_TIMESLOT",
                columns: table => new
                {
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CAPACITY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIMESLOT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF_TIMESLOT", x => new { x.STAFF_ID, x.DAY });
                    table.ForeignKey(
                        name: "FK_STAFF_TIMESLOT_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_TIMESLOT_TIME_SLOTS_TIMESLOT_ID",
                        column: x => x.TIMESLOT_ID,
                        principalTable: "TIME_SLOTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_TIMESLOT_TIMESLOT_ID",
                table: "STAFF_TIMESLOT",
                column: "TIMESLOT_ID");
        }
    }
}
