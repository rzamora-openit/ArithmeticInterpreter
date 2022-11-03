using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdateUserContractsPercentReductions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DailyHoursPercentReduction",
                table: "UserContracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PercentReduction",
                table: "UserContracts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyHoursPercentReduction",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "PercentReduction",
                table: "UserContracts");
        }
    }
}
