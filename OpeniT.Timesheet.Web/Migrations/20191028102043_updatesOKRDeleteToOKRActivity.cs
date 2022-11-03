using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class updatesOKRDeleteToOKRActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OKRDeletes");

            migrationBuilder.CreateTable(
                name: "OKRActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentDescription = table.Column<string>(nullable: true),
                    ActivityById = table.Column<int>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Activity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OKRActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OKRActivities_Users_ActivityById",
                        column: x => x.ActivityById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OKRActivities_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OKRActivities_ActivityById",
                table: "OKRActivities",
                column: "ActivityById");

            migrationBuilder.CreateIndex(
                name: "IX_OKRActivities_OwnerId",
                table: "OKRActivities",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OKRActivities");

            migrationBuilder.CreateTable(
                name: "OKRDeletes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activity = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DeletedById = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true),
                    ParentDescription = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
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
                name: "IX_OKRDeletes_OwnerId",
                table: "OKRDeletes",
                column: "OwnerId");
        }
    }
}
