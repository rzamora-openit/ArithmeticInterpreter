using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedHoursRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyHoursNeeded",
                table: "UserLocations",
                newName: "MonthlyHours");

            migrationBuilder.RenameColumn(
                name: "DailyHoursNeeded",
                table: "UserLocations",
                newName: "DailyHours");

            migrationBuilder.AddColumn<string>(
                name: "RequiredHours",
                table: "UserLocations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredHours",
                table: "UserLocations");

            migrationBuilder.RenameColumn(
                name: "MonthlyHours",
                table: "UserLocations",
                newName: "MonthlyHoursNeeded");

            migrationBuilder.RenameColumn(
                name: "DailyHours",
                table: "UserLocations",
                newName: "DailyHoursNeeded");
        }
    }
}
