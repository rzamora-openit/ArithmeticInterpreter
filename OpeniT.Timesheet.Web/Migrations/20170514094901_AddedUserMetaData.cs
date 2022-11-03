using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedUserMetaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineManagerId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LineManagerId",
                table: "Users",
                column: "LineManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_LineManagerId",
                table: "Users",
                column: "LineManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_LineManagerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LineManagerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LineManagerId",
                table: "Users");
        }
    }
}
