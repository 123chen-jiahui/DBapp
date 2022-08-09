using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_TIMESLOT_ID");

            migrationBuilder.AddColumn<int>(
                name: "TimeSlotId",
                table: "STAFF",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TIME_SLOTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    START_TIME = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    END_TIME = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIME_SLOTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STAFF_TIMESLOT",
                columns: table => new
                {
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
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
                name: "IX_STAFF_TimeSlotId",
                table: "STAFF",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_TIMESLOT_TIMESLOT_ID",
                table: "STAFF_TIMESLOT",
                column: "TIMESLOT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_STAFF_TIME_SLOTS_TimeSlotId",
                table: "STAFF",
                column: "TimeSlotId",
                principalTable: "TIME_SLOTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STAFF_TIME_SLOTS_TimeSlotId",
                table: "STAFF");

            migrationBuilder.DropTable(
                name: "STAFF_TIMESLOT");

            migrationBuilder.DropTable(
                name: "TIME_SLOTS");

            migrationBuilder.DropIndex(
                name: "IX_STAFF_TimeSlotId",
                table: "STAFF");

            migrationBuilder.DropSequence(
                name: "SEQ_TIMESLOT_ID");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "STAFF");
        }
    }
}
