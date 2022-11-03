using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatedObjectivesKeyResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjType",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Objectives",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Quarter",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "objGroup",
                table: "Objectives",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KrType",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KeyResultCheckins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KeyResultId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CheckinDate = table.Column<DateTime>(nullable: false),
                    CheckinPercentage = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Percent = table.Column<double>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResultCheckins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyResultCheckins_KeyResults_KeyResultId",
                        column: x => x.KeyResultId,
                        principalTable: "KeyResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyResultCheckins_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyResultCheckins_KeyResultId",
                table: "KeyResultCheckins",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResultCheckins_ObjectiveId",
                table: "KeyResultCheckins",
                column: "ObjectiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyResultCheckins");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "ObjType",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "Quarter",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "objGroup",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "KrType",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "KeyResults");
        }
    }
}
