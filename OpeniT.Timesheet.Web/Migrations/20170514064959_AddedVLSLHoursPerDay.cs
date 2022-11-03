using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedVLSLHoursPerDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SLHoursPerDay",
                table: "UserLocations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VLHoursPerDay",
                table: "UserLocations",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLHoursPerDay",
                table: "UserLocations");

            migrationBuilder.DropColumn(
                name: "VLHoursPerDay",
                table: "UserLocations");
        }
    }
}
