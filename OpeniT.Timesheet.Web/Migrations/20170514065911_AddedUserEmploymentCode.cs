using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedUserEmploymentCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmploymentCodeId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmploymentCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmploymentCodeId",
                table: "Users",
                column: "EmploymentCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EmploymentCodes_EmploymentCodeId",
                table: "Users",
                column: "EmploymentCodeId",
                principalTable: "EmploymentCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_EmploymentCodes_EmploymentCodeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EmploymentCodes");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmploymentCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmploymentCodeId",
                table: "Users");
        }
    }
}
