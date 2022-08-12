using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TIME",
                table: "REGISTRATIONS",
                newName: "CREATE_DATE_UTC");

            migrationBuilder.AddColumn<string>(
                name: "ROOM_ID",
                table: "REGISTRATIONS",
                type: "NVARCHAR2(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "STATE",
                table: "REGISTRATIONS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TRANSACTION_METADATA",
                table: "REGISTRATIONS",
                type: "NVARCHAR2(2000)",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "STATE",
                table: "REGISTRATIONS");

            migrationBuilder.DropColumn(
                name: "TRANSACTION_METADATA",
                table: "REGISTRATIONS");

            migrationBuilder.RenameColumn(
                name: "CREATE_DATE_UTC",
                table: "REGISTRATIONS",
                newName: "TIME");
        }
    }
}
