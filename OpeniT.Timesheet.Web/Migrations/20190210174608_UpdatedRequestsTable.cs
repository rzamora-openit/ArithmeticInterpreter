using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatedRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_ApprovedById",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "Requests",
                newName: "Days");

            migrationBuilder.RenameColumn(
                name: "ApprovedById",
                table: "Requests",
                newName: "AuthorizedById");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ApprovedById",
                table: "Requests",
                newName: "IX_Requests_AuthorizedById");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Requests",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "Requests",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AuthorizationDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_AuthorizedById",
                table: "Requests",
                column: "AuthorizedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_AuthorizedById",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AuthorizationDate",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "Requests",
                newName: "Hours");

            migrationBuilder.RenameColumn(
                name: "AuthorizedById",
                table: "Requests",
                newName: "ApprovedById");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AuthorizedById",
                table: "Requests",
                newName: "IX_Requests_ApprovedById");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Requests",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_ApprovedById",
                table: "Requests",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
