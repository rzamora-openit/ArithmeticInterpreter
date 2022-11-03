using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class RenamesStudentIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserThumbnails_Users_StudentId",
                table: "UserThumbnails");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "UserThumbnails",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserThumbnails_StudentId",
                table: "UserThumbnails",
                newName: "IX_UserThumbnails_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserThumbnails_Users_UserId",
                table: "UserThumbnails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserThumbnails_Users_UserId",
                table: "UserThumbnails");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserThumbnails",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_UserThumbnails_UserId",
                table: "UserThumbnails",
                newName: "IX_UserThumbnails_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserThumbnails_Users_StudentId",
                table: "UserThumbnails",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
