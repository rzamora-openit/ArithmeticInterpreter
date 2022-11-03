using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsDeviationReportSolutionsAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviationReportSolutionsAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    DeviationReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationReportSolutionsAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationReportSolutionsAttachments_DeviationReports_DeviationReportId",
                        column: x => x.DeviationReportId,
                        principalTable: "DeviationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationReportSolutionsAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportSolutionsAttachments_DeviationReportId",
                table: "DeviationReportSolutionsAttachments",
                column: "DeviationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportSolutionsAttachments_FileId",
                table: "DeviationReportSolutionsAttachments",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviationReportSolutionsAttachments");
        }
    }
}
