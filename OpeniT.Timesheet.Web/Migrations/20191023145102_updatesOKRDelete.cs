using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class updatesOKRDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OKRDeletes_KeyResults_KeyResultId",
                table: "OKRDeletes");

            migrationBuilder.DropForeignKey(
                name: "FK_OKRDeletes_Objectives_ObjectiveId",
                table: "OKRDeletes");

            migrationBuilder.DropIndex(
                name: "IX_OKRDeletes_KeyResultId",
                table: "OKRDeletes");

            migrationBuilder.DropIndex(
                name: "IX_OKRDeletes_ObjectiveId",
                table: "OKRDeletes");

            migrationBuilder.DropColumn(
                name: "KeyResultId",
                table: "OKRDeletes");

            migrationBuilder.DropColumn(
                name: "ObjectiveId",
                table: "OKRDeletes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyResultId",
                table: "OKRDeletes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObjectiveId",
                table: "OKRDeletes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OKRDeletes_KeyResultId",
                table: "OKRDeletes",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRDeletes_ObjectiveId",
                table: "OKRDeletes",
                column: "ObjectiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_OKRDeletes_KeyResults_KeyResultId",
                table: "OKRDeletes",
                column: "KeyResultId",
                principalTable: "KeyResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OKRDeletes_Objectives_ObjectiveId",
                table: "OKRDeletes",
                column: "ObjectiveId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
