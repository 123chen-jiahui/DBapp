using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateRegistration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRATIONS_ROOMS_ROOM_ID",
                table: "REGISTRATIONS");

            migrationBuilder.DropIndex(
                name: "IX_REGISTRATIONS_ROOM_ID",
                table: "REGISTRATIONS");

            migrationBuilder.DropColumn(
                name: "ROOM_ID",
                table: "REGISTRATIONS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ROOM_ID",
                table: "REGISTRATIONS",
                type: "NVARCHAR2(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_ROOM_ID",
                table: "REGISTRATIONS",
                column: "ROOM_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRATIONS_ROOMS_ROOM_ID",
                table: "REGISTRATIONS",
                column: "ROOM_ID",
                principalTable: "ROOMS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
