using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsAppraisalToolModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppraisalForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormName = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectionTitle = table.Column<string>(nullable: true),
                    SectionText = table.Column<string>(nullable: true),
                    SectionFor = table.Column<string>(nullable: true),
                    SectionOrder = table.Column<int>(nullable: false),
                    Inactive = table.Column<bool>(nullable: false),
                    AppraisalFormId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalSections_AppraisalForms_AppraisalFormId",
                        column: x => x.AppraisalFormId,
                        principalTable: "AppraisalForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalSpans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    PeriodFrom = table.Column<DateTime>(nullable: true),
                    PeriodTo = table.Column<DateTime>(nullable: true),
                    DateFrom = table.Column<DateTime>(nullable: true),
                    DateTo = table.Column<DateTime>(nullable: true),
                    FormId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalSpans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalSpans_AppraisalForms_FormId",
                        column: x => x.FormId,
                        principalTable: "AppraisalForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: true),
                    QuestionType = table.Column<string>(nullable: true),
                    QuestionNotes = table.Column<string>(nullable: true),
                    QuestionFor = table.Column<string>(nullable: true),
                    QuestionOrder = table.Column<int>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    AllowAttachments = table.Column<bool>(nullable: false),
                    Inactive = table.Column<bool>(nullable: false),
                    ParentQuestionId = table.Column<int>(nullable: true),
                    AppraisalSectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalQuestions_AppraisalSections_AppraisalSectionId",
                        column: x => x.AppraisalSectionId,
                        principalTable: "AppraisalSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalQuestions_AppraisalQuestions_ParentQuestionId",
                        column: x => x.ParentQuestionId,
                        principalTable: "AppraisalQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedBy = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    SubmittedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    SpanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appraisals_AppraisalSpans_SpanId",
                        column: x => x.SpanId,
                        principalTable: "AppraisalSpans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appraisals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalQuestionAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalQuestionAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalQuestionAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalQuestionAttachments_AppraisalQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AppraisalQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalAnswerLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedBy = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    AnswerFor = table.Column<string>(nullable: true),
                    AppraisalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalAnswerLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalAnswerLists_Appraisals_AppraisalId",
                        column: x => x.AppraisalId,
                        principalTable: "Appraisals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    AnswerOrder = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: true),
                    AppraisalAnswerListId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalAnswers_AppraisalAnswerLists_AppraisalAnswerListId",
                        column: x => x.AppraisalAnswerListId,
                        principalTable: "AppraisalAnswerLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalAnswers_AppraisalQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AppraisalQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalAnswerAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: true),
                    AnswerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalAnswerAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppraisalAnswerAttachments_AppraisalAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "AppraisalAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalAnswerAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalAnswerAttachments_AnswerId",
                table: "AppraisalAnswerAttachments",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalAnswerAttachments_FileId",
                table: "AppraisalAnswerAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalAnswerLists_AppraisalId",
                table: "AppraisalAnswerLists",
                column: "AppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalAnswers_AppraisalAnswerListId",
                table: "AppraisalAnswers",
                column: "AppraisalAnswerListId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalAnswers_QuestionId",
                table: "AppraisalAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalQuestionAttachments_FileId",
                table: "AppraisalQuestionAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalQuestionAttachments_QuestionId",
                table: "AppraisalQuestionAttachments",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalQuestions_AppraisalSectionId",
                table: "AppraisalQuestions",
                column: "AppraisalSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalQuestions_ParentQuestionId",
                table: "AppraisalQuestions",
                column: "ParentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_SpanId",
                table: "Appraisals",
                column: "SpanId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_UserId",
                table: "Appraisals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalSections_AppraisalFormId",
                table: "AppraisalSections",
                column: "AppraisalFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalSpans_FormId",
                table: "AppraisalSpans",
                column: "FormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppraisalAnswerAttachments");

            migrationBuilder.DropTable(
                name: "AppraisalQuestionAttachments");

            migrationBuilder.DropTable(
                name: "AppraisalAnswers");

            migrationBuilder.DropTable(
                name: "AppraisalAnswerLists");

            migrationBuilder.DropTable(
                name: "AppraisalQuestions");

            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "AppraisalSections");

            migrationBuilder.DropTable(
                name: "AppraisalSpans");

            migrationBuilder.DropTable(
                name: "AppraisalForms");
        }
    }
}
