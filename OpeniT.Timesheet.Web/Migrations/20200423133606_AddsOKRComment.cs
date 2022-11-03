using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsOKRComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OKRComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Edited = table.Column<DateTimeOffset>(nullable: false),
                    Deleted = table.Column<DateTimeOffset>(nullable: false),
                    KeyResultId = table.Column<int>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    HasChildren = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OKRComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OKRComments_KeyResults_KeyResultId",
                        column: x => x.KeyResultId,
                        principalTable: "KeyResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRComments_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRComments_OKRComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "OKRComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OKRComments_KeyResultId",
                table: "OKRComments",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRComments_ObjectiveId",
                table: "OKRComments",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRComments_ParentId",
                table: "OKRComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRComments_UserId",
                table: "OKRComments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OKRComments");
        }
    }
}
