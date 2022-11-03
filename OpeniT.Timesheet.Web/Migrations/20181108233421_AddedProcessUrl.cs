using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedProcessUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProcessUrl",
                table: "DeviationReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "DeviationReports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReports_UpdatedById",
                table: "DeviationReports",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviationReports_Users_UpdatedById",
                table: "DeviationReports",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviationReports_Users_UpdatedById",
                table: "DeviationReports");

            migrationBuilder.DropIndex(
                name: "IX_DeviationReports_UpdatedById",
                table: "DeviationReports");

            migrationBuilder.DropColumn(
                name: "ProcessUrl",
                table: "DeviationReports");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "DeviationReports");
        }
    }
}
