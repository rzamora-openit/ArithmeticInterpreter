using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsFileAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    UploadedById = table.Column<int>(nullable: true),
                    Binaries = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviationActivitytAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    DeviationActivityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationActivitytAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationActivitytAttachments_DeviationActivities_DeviationActivityId",
                        column: x => x.DeviationActivityId,
                        principalTable: "DeviationActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationActivitytAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviationReportAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    DeviationReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationReportAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationReportAttachments_DeviationReports_DeviationReportId",
                        column: x => x.DeviationReportId,
                        principalTable: "DeviationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationReportAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviationActivitytAttachments_DeviationActivityId",
                table: "DeviationActivitytAttachments",
                column: "DeviationActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationActivitytAttachments_FileId",
                table: "DeviationActivitytAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportAttachments_DeviationReportId",
                table: "DeviationReportAttachments",
                column: "DeviationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportAttachments_FileId",
                table: "DeviationReportAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UploadedById",
                table: "Files",
                column: "UploadedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviationActivitytAttachments");

            migrationBuilder.DropTable(
                name: "DeviationReportAttachments");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
