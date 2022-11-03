using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class addsOKRDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OKRDeletes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentDescription = table.Column<string>(nullable: true),
                    DeletedById = table.Column<int>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Activity = table.Column<string>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true),
                    KeyResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OKRDeletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OKRDeletes_Users_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRDeletes_KeyResults_KeyResultId",
                        column: x => x.KeyResultId,
                        principalTable: "KeyResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRDeletes_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRDeletes_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OKRDeletes_DeletedById",
                table: "OKRDeletes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_OKRDeletes_KeyResultId",
                table: "OKRDeletes",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRDeletes_ObjectiveId",
                table: "OKRDeletes",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRDeletes_OwnerId",
                table: "OKRDeletes",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OKRDeletes");
        }
    }
}
