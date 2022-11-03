using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedProcessOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_ResponsibleId",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_ResponsibleId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Objectives");

            migrationBuilder.AddColumn<int>(
                name: "DeputyOwnerId",
                table: "SubProcesses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "SubProcesses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeputyOwnerId",
                table: "Processes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Processes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProcesses_DeputyOwnerId",
                table: "SubProcesses",
                column: "DeputyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProcesses_OwnerId",
                table: "SubProcesses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_DeputyOwnerId",
                table: "Processes",
                column: "DeputyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_OwnerId",
                table: "Processes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Users_DeputyOwnerId",
                table: "Processes",
                column: "DeputyOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Users_OwnerId",
                table: "Processes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProcesses_Users_DeputyOwnerId",
                table: "SubProcesses",
                column: "DeputyOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProcesses_Users_OwnerId",
                table: "SubProcesses",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Users_DeputyOwnerId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Users_OwnerId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProcesses_Users_DeputyOwnerId",
                table: "SubProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProcesses_Users_OwnerId",
                table: "SubProcesses");

            migrationBuilder.DropIndex(
                name: "IX_SubProcesses_DeputyOwnerId",
                table: "SubProcesses");

            migrationBuilder.DropIndex(
                name: "IX_SubProcesses_OwnerId",
                table: "SubProcesses");

            migrationBuilder.DropIndex(
                name: "IX_Processes_DeputyOwnerId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_OwnerId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "DeputyOwnerId",
                table: "SubProcesses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "SubProcesses");

            migrationBuilder.DropColumn(
                name: "DeputyOwnerId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleId",
                table: "Objectives",
                nullable: true);

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
    }
}
