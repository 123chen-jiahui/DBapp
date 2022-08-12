using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateRegistration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DAY",
                table: "REGISTRATIONS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ORDER",
                table: "REGISTRATIONS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ROOM_ID",
                table: "REGISTRATIONS",
                type: "NVARCHAR2(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TIME",
                table: "REGISTRATIONS",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "DAY",
                table: "REGISTRATIONS");

            migrationBuilder.DropColumn(
                name: "ORDER",
                table: "REGISTRATIONS");

            migrationBuilder.DropColumn(
                name: "ROOM_ID",
                table: "REGISTRATIONS");

            migrationBuilder.DropColumn(
                name: "TIME",
                table: "REGISTRATIONS");
        }
    }
}
