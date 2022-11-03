using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsObjectiveAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObjectiveAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectiveId = table.Column<int>(nullable: true),
                    ProcessId = table.Column<int>(nullable: true),
                    AssignedId = table.Column<int>(nullable: true),
                    Assignment = table.Column<string>(nullable: true),
                    IsRevoked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectiveAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectiveAssignments_Users_AssignedId",
                        column: x => x.AssignedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObjectiveAssignments_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObjectiveAssignments_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveAssignments_AssignedId",
                table: "ObjectiveAssignments",
                column: "AssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveAssignments_ObjectiveId",
                table: "ObjectiveAssignments",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveAssignments_ProcessId",
                table: "ObjectiveAssignments",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectiveAssignments");
        }
    }
}
