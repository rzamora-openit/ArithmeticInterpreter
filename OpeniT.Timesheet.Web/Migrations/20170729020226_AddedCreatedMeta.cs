using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedCreatedMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pid",
                table: "TaskGroups");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TaskGroups",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TaskGroups",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "TaskGroups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "TaskGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskGroups_ProcessId",
                table: "TaskGroups",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskGroups_Processes_ProcessId",
                table: "TaskGroups",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskGroups_Processes_ProcessId",
                table: "TaskGroups");

            migrationBuilder.DropIndex(
                name: "IX_TaskGroups_ProcessId",
                table: "TaskGroups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TaskGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TaskGroups");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "TaskGroups");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "TaskGroups");

            migrationBuilder.AddColumn<int>(
                name: "Pid",
                table: "TaskGroups",
                nullable: false,
                defaultValue: 0);
        }
    }
}
