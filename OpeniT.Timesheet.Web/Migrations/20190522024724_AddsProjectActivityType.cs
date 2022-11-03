using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsProjectActivityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivities_Users_OwnerId",
                table: "ProjectActivities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectActivities");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "ProjectActivities",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectActivities_OwnerId",
                table: "ProjectActivities",
                newName: "IX_ProjectActivities_UserId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "ProjectActivities",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ProjectActivities",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivities_Users_UserId",
                table: "ProjectActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivities_Users_UserId",
                table: "ProjectActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ProjectActivities");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProjectActivities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectActivities",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectActivities_UserId",
                table: "ProjectActivities",
                newName: "IX_ProjectActivities_OwnerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectActivities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivities_Users_OwnerId",
                table: "ProjectActivities",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
