using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class RenamedMidshiftToNSTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MidshiftSummaries_Users_UserId",
                table: "MidshiftSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MidshiftSummaries",
                table: "MidshiftSummaries");

            migrationBuilder.RenameTable(
                name: "MidshiftSummaries",
                newName: "NSTS");

            migrationBuilder.RenameIndex(
                name: "IX_MidshiftSummaries_UserId",
                table: "NSTS",
                newName: "IX_NSTS_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NSTS",
                table: "NSTS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NSTS_Users_UserId",
                table: "NSTS",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NSTS_Users_UserId",
                table: "NSTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NSTS",
                table: "NSTS");

            migrationBuilder.RenameTable(
                name: "NSTS",
                newName: "MidshiftSummaries");

            migrationBuilder.RenameIndex(
                name: "IX_NSTS_UserId",
                table: "MidshiftSummaries",
                newName: "IX_MidshiftSummaries_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MidshiftSummaries",
                table: "MidshiftSummaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MidshiftSummaries_Users_UserId",
                table: "MidshiftSummaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
