using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatesProjectSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountManager",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRequested",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modules",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectManager",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resources",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Server",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TpaResources",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_OwnerId",
                table: "ProjectActivities",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ProjectId",
                table: "ProjectActivities",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectActivities");

            migrationBuilder.DropColumn(
                name: "AccountManager",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateRequested",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Modules",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectManager",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Resources",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Server",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TpaResources",
                table: "Projects");
        }
    }
}
