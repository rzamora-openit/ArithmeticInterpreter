using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedUserContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowedSLDays = table.Column<double>(nullable: false),
                    AllowedVLDays = table.Column<double>(nullable: false),
                    DailyHoursRequired = table.Column<double>(nullable: false),
                    MonthlyHoursRequired = table.Column<double>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: true),
                    ValidUntil = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserContracts_UserId",
                table: "UserContracts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserContracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Users",
                nullable: true);
        }
    }
}
