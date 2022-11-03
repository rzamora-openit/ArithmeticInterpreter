using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsRequestAcknoledgement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcknowledgedById",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AcknowledgementDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AcknowledgedById",
                table: "Requests",
                column: "AcknowledgedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_AcknowledgedById",
                table: "Requests",
                column: "AcknowledgedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_AcknowledgedById",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AcknowledgedById",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AcknowledgedById",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AcknowledgementDate",
                table: "Requests");
        }
    }
}
