using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsDeviationReportSolutions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongTermSolution",
                table: "DeviationReports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortTermSolution",
                table: "DeviationReports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongTermSolution",
                table: "DeviationReports");

            migrationBuilder.DropColumn(
                name: "ShortTermSolution",
                table: "DeviationReports");
        }
    }
}
