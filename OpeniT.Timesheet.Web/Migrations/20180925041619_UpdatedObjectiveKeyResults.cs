using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatedObjectiveKeyResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Objectives",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleId",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "KeyResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ResponsibleId",
                table: "Objectives",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_ResponsibleId",
                table: "Objectives",
                column: "ResponsibleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_ResponsibleId",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_ResponsibleId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "KeyResults");
        }
    }
}
