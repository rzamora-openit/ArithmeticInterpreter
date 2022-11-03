using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedRecordSummaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Difference = table.Column<double>(nullable: false),
                    ExcessHours = table.Column<double>(nullable: false),
                    Hours = table.Column<double>(nullable: false),
                    Month = table.Column<DateTime>(nullable: false),
                    RequiredHours = table.Column<double>(nullable: false),
                    SalaryDeduction = table.Column<double>(nullable: false),
                    TotalHours = table.Column<double>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordSummaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordSummaries_UserId",
                table: "RecordSummaries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordSummaries");
        }
    }
}
