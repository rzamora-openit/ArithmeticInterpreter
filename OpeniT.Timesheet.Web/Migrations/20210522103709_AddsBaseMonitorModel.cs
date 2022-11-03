using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsBaseMonitorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "UserContracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "UserContracts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "UserContracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "UserContracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "RecordSummaries",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "RecordSummaries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "RecordSummaries",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "RecordSummaries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "UserContracts");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "RecordSummaries");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "RecordSummaries");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "RecordSummaries");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "RecordSummaries");
        }
    }
}
