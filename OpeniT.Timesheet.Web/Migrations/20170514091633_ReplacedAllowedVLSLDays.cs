using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class ReplacedAllowedVLSLDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VLHoursPerDay",
                table: "UserLocations",
                newName: "AllowedVLDays");

            migrationBuilder.RenameColumn(
                name: "SLHoursPerDay",
                table: "UserLocations",
                newName: "AllowedSLDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllowedVLDays",
                table: "UserLocations",
                newName: "VLHoursPerDay");

            migrationBuilder.RenameColumn(
                name: "AllowedSLDays",
                table: "UserLocations",
                newName: "SLHoursPerDay");
        }
    }
}
