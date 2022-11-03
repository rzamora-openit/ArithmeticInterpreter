using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedParentObjectiveRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Objectives",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ParentId",
                table: "Objectives",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Objectives_ParentId",
                table: "Objectives",
                column: "ParentId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Objectives_ParentId",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_ParentId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Objectives");
        }
    }
}
