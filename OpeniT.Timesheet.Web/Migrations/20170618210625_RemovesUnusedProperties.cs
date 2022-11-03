using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class RemovesUnusedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedSLDays",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AllowedVLDays",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DailyHoursRequired",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonthlyHoursRequired",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AllowedSLDays",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AllowedVLDays",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DailyHoursRequired",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MonthlyHoursRequired",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
