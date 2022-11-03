using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedPinnedRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PinnedRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Hours = table.Column<double>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    SubProcessId = table.Column<int>(nullable: true),
                    SubTask = table.Column<string>(nullable: true),
                    Task = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinnedRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinnedRecords_SubProcesses_SubProcessId",
                        column: x => x.SubProcessId,
                        principalTable: "SubProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PinnedRecords_SubProcessId",
                table: "PinnedRecords",
                column: "SubProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PinnedRecords");
        }
    }
}
