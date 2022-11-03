using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdateUserContractExcessHoursMonthLimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExcessHoursMonthLimit",
                table: "UserLocations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ExcessHoursMonthLimit",
                table: "UserContracts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcessHoursMonthLimit",
                table: "UserLocations");

            migrationBuilder.DropColumn(
                name: "ExcessHoursMonthLimit",
                table: "UserContracts");
        }
    }
}
