using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedSubProcessDeviation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SeenBySubOwner",
                table: "DeviationReports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SubProcessId",
                table: "DeviationReports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReports_SubProcessId",
                table: "DeviationReports",
                column: "SubProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviationReports_SubProcesses_SubProcessId",
                table: "DeviationReports",
                column: "SubProcessId",
                principalTable: "SubProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviationReports_SubProcesses_SubProcessId",
                table: "DeviationReports");

            migrationBuilder.DropIndex(
                name: "IX_DeviationReports_SubProcessId",
                table: "DeviationReports");

            migrationBuilder.DropColumn(
                name: "SeenBySubOwner",
                table: "DeviationReports");

            migrationBuilder.DropColumn(
                name: "SubProcessId",
                table: "DeviationReports");
        }
    }
}
