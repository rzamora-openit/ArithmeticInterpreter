using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedHangfireToRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HangfireJobId",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertedById",
                table: "Records",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_InsertedById",
                table: "Records",
                column: "InsertedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Users_InsertedById",
                table: "Records",
                column: "InsertedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_InsertedById",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_InsertedById",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "HangfireJobId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "InsertedById",
                table: "Records");
        }
    }
}
