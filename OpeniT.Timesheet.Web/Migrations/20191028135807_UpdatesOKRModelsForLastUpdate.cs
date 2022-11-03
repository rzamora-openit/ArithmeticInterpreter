using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatesOKRModelsForLastUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastCheckin",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastCheckinById",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckinById",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastCheckin",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_LastCheckinById",
                table: "Objectives",
                column: "LastCheckinById");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_UpdatedById",
                table: "Objectives",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_CheckinById",
                table: "KeyResults",
                column: "CheckinById");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_UpdatedById",
                table: "KeyResults",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResults_Users_CheckinById",
                table: "KeyResults",
                column: "CheckinById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResults_Users_UpdatedById",
                table: "KeyResults",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_LastCheckinById",
                table: "Objectives",
                column: "LastCheckinById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_UpdatedById",
                table: "Objectives",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyResults_Users_CheckinById",
                table: "KeyResults");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyResults_Users_UpdatedById",
                table: "KeyResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_LastCheckinById",
                table: "Objectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_UpdatedById",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_LastCheckinById",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_UpdatedById",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_KeyResults_CheckinById",
                table: "KeyResults");

            migrationBuilder.DropIndex(
                name: "IX_KeyResults_UpdatedById",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "LastCheckin",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "LastCheckinById",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "CheckinById",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "LastCheckin",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "KeyResults");
        }
    }
}
