using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateRegistration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CREATE_DATE_UTC",
                table: "REGISTRATIONS",
                newName: "CREATE_DATE_LOCAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CREATE_DATE_LOCAL",
                table: "REGISTRATIONS",
                newName: "CREATE_DATE_UTC");
        }
    }
}
