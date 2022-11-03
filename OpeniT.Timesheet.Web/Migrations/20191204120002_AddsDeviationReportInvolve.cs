using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsDeviationReportInvolve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviationReportInvolves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviationReportId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationReportInvolves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationReportInvolves_DeviationReports_DeviationReportId",
                        column: x => x.DeviationReportId,
                        principalTable: "DeviationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationReportInvolves_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportInvolves_DeviationReportId",
                table: "DeviationReportInvolves",
                column: "DeviationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportInvolves_UserId",
                table: "DeviationReportInvolves",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviationReportInvolves");
        }
    }
}
