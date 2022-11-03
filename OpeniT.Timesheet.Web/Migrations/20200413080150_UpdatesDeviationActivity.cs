using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatesDeviationActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "DeviationActivities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviationActivities_ParentId",
                table: "DeviationActivities",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviationActivities_DeviationActivities_ParentId",
                table: "DeviationActivities",
                column: "ParentId",
                principalTable: "DeviationActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviationActivities_DeviationActivities_ParentId",
                table: "DeviationActivities");

            migrationBuilder.DropIndex(
                name: "IX_DeviationActivities_ParentId",
                table: "DeviationActivities");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "DeviationActivities");
        }
    }
}
