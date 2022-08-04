using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ChangeMedicine2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MANUFACTURER",
                table: "MEDICINE",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MANUFACTURER",
                table: "MEDICINE");
        }
    }
}
