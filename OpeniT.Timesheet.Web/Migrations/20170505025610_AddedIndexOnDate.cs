using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedIndexOnDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_Owner",
                table: "Records");

            migrationBuilder.AddColumn<double>(
                name: "DailyHoursNeeded",
                table: "UserLocations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DailyHoursRequired",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ExcessHours",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyHoursRequired",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Records_Owner_Date",
                table: "Records",
                columns: new[] { "Owner", "Date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_Owner_Date",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "DailyHoursNeeded",
                table: "UserLocations");

            migrationBuilder.DropColumn(
                name: "DailyHoursRequired",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExcessHours",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonthlyHoursRequired",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Records_Owner",
                table: "Records",
                column: "Owner");
        }
    }
}
