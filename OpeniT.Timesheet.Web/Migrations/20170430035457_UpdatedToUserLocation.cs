using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatedToUserLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserLocations_OfficeLocationId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "OfficeLocationId",
                table: "Users",
                newName: "UserLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_OfficeLocationId",
                table: "Users",
                newName: "IX_Users_UserLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserLocations_UserLocationId",
                table: "Users",
                column: "UserLocationId",
                principalTable: "UserLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserLocations_UserLocationId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserLocationId",
                table: "Users",
                newName: "OfficeLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserLocationId",
                table: "Users",
                newName: "IX_Users_OfficeLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserLocations_OfficeLocationId",
                table: "Users",
                column: "OfficeLocationId",
                principalTable: "UserLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
