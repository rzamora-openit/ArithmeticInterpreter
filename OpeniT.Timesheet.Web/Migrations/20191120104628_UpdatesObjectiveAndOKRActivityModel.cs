using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatesObjectiveAndOKRActivityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedField",
                table: "OKRActivities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Objectives",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_CreatedById",
                table: "Objectives",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_CreatedById",
                table: "Objectives",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_CreatedById",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_CreatedById",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "UpdatedField",
                table: "OKRActivities");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Objectives");
        }
    }
}
