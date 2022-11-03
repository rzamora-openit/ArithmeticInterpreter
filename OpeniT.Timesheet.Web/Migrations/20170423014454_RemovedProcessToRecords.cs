using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class RemovedProcessToRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Processes_ProcessId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ProcessId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Records");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "Records",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_ProcessId",
                table: "Records",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Processes_ProcessId",
                table: "Records",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
