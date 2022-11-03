using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsKeyResultsScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelfAssessment",
                table: "KeyResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "SelfAssessment",
                table: "KeyResults");
        }
    }
}
