using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class RemoveNotStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppraisalAnswerAttachments");

            migrationBuilder.DropTable(
                name: "AppraisalQuestionAttachments");

            migrationBuilder.DropTable(
                name: "CheckinConversation");

            migrationBuilder.DropTable(
                name: "DeviationActivitytAttachments");

            migrationBuilder.DropTable(
                name: "DeviationReportAttachments");

            migrationBuilder.DropTable(
                name: "DeviationReportInvolves");

            migrationBuilder.DropTable(
                name: "DeviationReportSolutionsAttachments");

            migrationBuilder.DropTable(
                name: "EmailRedirects");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NSTS");

            migrationBuilder.DropTable(
                name: "ObjectiveAssignments");

            migrationBuilder.DropTable(
                name: "OKRActivities");

            migrationBuilder.DropTable(
                name: "OKRComments");

            migrationBuilder.DropTable(
                name: "PinnedRecords");

            migrationBuilder.DropTable(
                name: "ProjectActivities");

            migrationBuilder.DropTable(
                name: "ProjectStatus");

            migrationBuilder.DropTable(
                name: "ProjectTypes");

            migrationBuilder.DropTable(
                name: "TimeLogs");

            migrationBuilder.DropTable(
                name: "TimeLogSummaries");

            migrationBuilder.DropTable(
                name: "AppraisalAnswers");

            migrationBuilder.DropTable(
                name: "Checkins");

            migrationBuilder.DropTable(
                name: "DeviationActivities");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "PublishedEvents");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "AppraisalAnswerLists");

            migrationBuilder.DropTable(
                name: "AppraisalQuestions");

            migrationBuilder.DropTable(
                name: "KeyResults");

            migrationBuilder.DropTable(
                name: "DeviationReports");

            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "AppraisalSections");

            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "AppraisalSpans");

            migrationBuilder.DropTable(
                name: "AppraisalForms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppraisalForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormName = table.Column<string>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviationReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Access = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DateTransferred = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LongTermSolution = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    ProcessId = table.Column<int>(nullable: true),
                    ProcessUrl = table.Column<string>(nullable: true),
                    ReportedById = table.Column<int>(nullable: true),
                    SeenByOwner = table.Column<bool>(nullable: false),
                    SeenBySubOwner = table.Column<bool>(nullable: false),
                    ShortTermSolution = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SubProcessId = table.Column<int>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationReports_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationReports_Users_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationReports_SubProcesses_SubProcessId",
                        column: x => x.SubProcessId,
                        principalTable: "SubProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailRedirects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedBy = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    DelegateApproval = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    RedirectEmail = table.Column<string>(nullable: true),
                    RedirectOption = table.Column<string>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: true),
                    ValidUntil = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailRedirects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Binaries = table.Column<byte[]>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    UploadedById = table.Column<int>(nullable: true)
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
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateIssued = table.Column<DateTimeOffset>(nullable: false),
                    DateNotified = table.Column<DateTimeOffset>(nullable: false),
                    DateSeen = table.Column<DateTimeOffset>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    ForUserId = table.Column<int>(nullable: true),
                    Importance = table.Column<int>(nullable: false),
                    Notified = table.Column<bool>(nullable: false),
                    OriginCreated = table.Column<DateTimeOffset>(nullable: false),
                    OriginIdentifier = table.Column<int>(nullable: false),
                    OriginType = table.Column<string>(nullable: true),
                    OriginUserId = table.Column<int>(nullable: true),
                    Param1 = table.Column<string>(nullable: true),
                    Param2 = table.Column<string>(nullable: true),
                    Param3 = table.Column<string>(nullable: true),
                    RelevantInfo = table.Column<string>(nullable: true),
                    Seen = table.Column<bool>(nullable: false),
                    Superseded = table.Column<bool>(nullable: false),
                    SupersededById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_ForUserId",
                        column: x => x.ForUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_OriginUserId",
                        column: x => x.OriginUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Notifications_SupersededById",
                        column: x => x.SupersededById,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NSTS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Midshift = table.Column<int>(nullable: false),
                    Month = table.Column<DateTime>(nullable: false),
                    Nightshift = table.Column<int>(nullable: false),
                    SubmittedOn = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NSTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NSTS_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true),
                    CommentCount = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastCheckin = table.Column<DateTimeOffset>(nullable: true),
                    LastCheckinById = table.Column<int>(nullable: true),
                    LastUpdate = table.Column<DateTimeOffset>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ObjType = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProcessId = table.Column<int>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    UpdatedById = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    objGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objectives_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objectives_Users_LastCheckinById",
                        column: x => x.LastCheckinById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objectives_Objectives_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objectives_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objectives_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objectives_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OKRActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activity = table.Column<string>(nullable: true),
                    ActivityById = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    ObjectId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true),
                    ParentDescription = table.Column<string>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UpdatedField = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PinnedRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Hours = table.Column<double>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    OrderBy = table.Column<int>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    SubProcessId = table.Column<int>(nullable: true),
                    SubTask = table.Column<string>(nullable: true),
                    Task = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinnedRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinnedRecords_SubProcesses_SubProcessId",
                        column: x => x.SubProcessId,
                        principalTable: "SubProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountManager = table.Column<string>(nullable: true),
                    Base = table.Column<string>(nullable: true),
                    DateRequested = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDone = table.Column<bool>(nullable: false),
                    Modules = table.Column<string>(nullable: true),
                    Package = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectManager = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Resources = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TpaResources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Selected = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AffectedRows = table.Column<int>(nullable: false),
                    EndedAt = table.Column<DateTimeOffset>(nullable: true),
                    HangfireJobId = table.Column<int>(nullable: false),
                    PublishedById = table.Column<int>(nullable: true),
                    StartedAt = table.Column<DateTimeOffset>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedEvents_Users_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<double>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    TimeIn = table.Column<TimeSpan>(nullable: true),
                    TimeOut = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeLogSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<DateTime>(nullable: false),
                    NightDiffTotal = table.Column<double>(nullable: false),
                    SubmittedOn = table.Column<DateTimeOffset>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLogSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeLogSummaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalSections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppraisalFormId = table.Column<int>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    SectionFor = table.Column<string>(nullable: true),
                    SectionOrder = table.Column<int>(nullable: false),
                    SectionText = table.Column<string>(nullable: true),
                    SectionTitle = table.Column<string>(nullable: true)
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
                    DateFrom = table.Column<DateTime>(nullable: true),
                    DateTo = table.Column<DateTime>(nullable: true),
                    FormId = table.Column<int>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    PeriodFrom = table.Column<DateTime>(nullable: true),
                    PeriodTo = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
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
                name: "DeviationActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DeviationReportId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationActivities_DeviationReports_DeviationReportId",
                        column: x => x.DeviationReportId,
                        principalTable: "DeviationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationActivities_DeviationActivities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DeviationActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviationActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviationReportInvolves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviationReportId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "DeviationReportAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviationReportId = table.Column<int>(nullable: true),
                    FileId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileTypeInfo = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "DeviationReportSolutionsAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviationReportId = table.Column<int>(nullable: true),
                    FileId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileTypeInfo = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "KeyResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckinById = table.Column<int>(nullable: true),
                    CommentCount = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    KrType = table.Column<string>(nullable: true),
                    LastCheckin = table.Column<DateTimeOffset>(nullable: true),
                    LastComment = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTimeOffset>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true),
                    Percentage = table.Column<double>(nullable: false),
                    Progress = table.Column<double>(nullable: false),
                    ProgressType = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Score = table.Column<double>(nullable: true),
                    SelfAssessment = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TargetPercentage = table.Column<double>(nullable: false),
                    TargetQuantity = table.Column<double>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyResults_Users_CheckinById",
                        column: x => x.CheckinById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyResults_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObjectiveAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignedId = table.Column<int>(nullable: true),
                    Assignment = table.Column<string>(nullable: true),
                    IsRevoked = table.Column<bool>(nullable: false),
                    ObjectiveId = table.Column<int>(nullable: true),
                    ProcessId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProjectActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllDay = table.Column<bool>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    PublishedEventId = table.Column<int>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_PublishedEvents_PublishedEventId",
                        column: x => x.PublishedEventId,
                        principalTable: "PublishedEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_UserLocations_UserLocationId",
                        column: x => x.UserLocationId,
                        principalTable: "UserLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowAttachments = table.Column<bool>(nullable: false),
                    AppraisalSectionId = table.Column<int>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    ParentQuestionId = table.Column<int>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    QuestionFor = table.Column<string>(nullable: true),
                    QuestionNotes = table.Column<string>(nullable: true),
                    QuestionOrder = table.Column<int>(nullable: false),
                    QuestionType = table.Column<string>(nullable: true),
                    Required = table.Column<bool>(nullable: false)
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
                    SpanId = table.Column<int>(nullable: true),
                    SubmittedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
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
                name: "DeviationActivitytAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviationActivityId = table.Column<int>(nullable: true),
                    FileId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileTypeInfo = table.Column<string>(nullable: true)
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
                name: "Checkins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckinIssueDate = table.Column<DateTime>(nullable: false),
                    CheckinPercentage = table.Column<double>(nullable: false),
                    CheckinQuantity = table.Column<double>(nullable: false),
                    CheckinType = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    KeyResultId = table.Column<int>(nullable: true),
                    LineManagerId = table.Column<int>(nullable: true),
                    LmCheckinDate = table.Column<DateTime>(nullable: false),
                    LmComment = table.Column<string>(nullable: true),
                    LmCommentLocked = table.Column<bool>(nullable: false),
                    ObjectiveId = table.Column<int>(nullable: true),
                    OwnerCheckinDate = table.Column<DateTime>(nullable: false),
                    OwnerComment = table.Column<string>(nullable: true),
                    OwnerCommentLocked = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkins_KeyResults_KeyResultId",
                        column: x => x.KeyResultId,
                        principalTable: "KeyResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checkins_Users_LineManagerId",
                        column: x => x.LineManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checkins_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checkins_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OKRComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Deleted = table.Column<DateTimeOffset>(nullable: false),
                    Edited = table.Column<DateTimeOffset>(nullable: false),
                    HasChildren = table.Column<bool>(nullable: false),
                    KeyResultId = table.Column<int>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AppraisalQuestionAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileTypeInfo = table.Column<string>(nullable: true),
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
                    AnswerFor = table.Column<string>(nullable: true),
                    AppraisalId = table.Column<int>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true)
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
                name: "CheckinConversation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckinId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(nullable: false),
                    DateEdited = table.Column<DateTimeOffset>(nullable: false),
                    KeyResultId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckinConversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckinConversation_Checkins_CheckinId",
                        column: x => x.CheckinId,
                        principalTable: "Checkins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckinConversation_KeyResults_KeyResultId",
                        column: x => x.KeyResultId,
                        principalTable: "KeyResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckinConversation_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckinConversation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    AnswerOrder = table.Column<int>(nullable: false),
                    AppraisalAnswerListId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true)
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
                    AnswerId = table.Column<int>(nullable: true),
                    FileId = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    FileTypeInfo = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_CheckinConversation_CheckinId",
                table: "CheckinConversation",
                column: "CheckinId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinConversation_KeyResultId",
                table: "CheckinConversation",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinConversation_ObjectiveId",
                table: "CheckinConversation",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinConversation_UserId",
                table: "CheckinConversation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_KeyResultId",
                table: "Checkins",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_LineManagerId",
                table: "Checkins",
                column: "LineManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_ObjectiveId",
                table: "Checkins",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_OwnerId",
                table: "Checkins",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationActivities_DeviationReportId",
                table: "DeviationActivities",
                column: "DeviationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationActivities_ParentId",
                table: "DeviationActivities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationActivities_UserId",
                table: "DeviationActivities",
                column: "UserId");

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
                name: "IX_DeviationReportInvolves_DeviationReportId",
                table: "DeviationReportInvolves",
                column: "DeviationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportInvolves_UserId",
                table: "DeviationReportInvolves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReports_ProcessId",
                table: "DeviationReports",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReports_ReportedById",
                table: "DeviationReports",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReports_SubProcessId",
                table: "DeviationReports",
                column: "SubProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReports_UpdatedById",
                table: "DeviationReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportSolutionsAttachments_DeviationReportId",
                table: "DeviationReportSolutionsAttachments",
                column: "DeviationReportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationReportSolutionsAttachments_FileId",
                table: "DeviationReportSolutionsAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PublishedEventId",
                table: "Events",
                column: "PublishedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserLocationId",
                table: "Events",
                column: "UserLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UploadedById",
                table: "Files",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_CheckinById",
                table: "KeyResults",
                column: "CheckinById");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_ObjectiveId",
                table: "KeyResults",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_UpdatedById",
                table: "KeyResults",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ForUserId",
                table: "Notifications",
                column: "ForUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OriginUserId",
                table: "Notifications",
                column: "OriginUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SupersededById",
                table: "Notifications",
                column: "SupersededById",
                unique: true,
                filter: "[SupersededById] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NSTS_UserId",
                table: "NSTS",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_CreatedById",
                table: "Objectives",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_LastCheckinById",
                table: "Objectives",
                column: "LastCheckinById");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ParentId",
                table: "Objectives",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ProcessId",
                table: "Objectives",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_UpdatedById",
                table: "Objectives",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_UserId",
                table: "Objectives",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OKRActivities_ActivityById",
                table: "OKRActivities",
                column: "ActivityById");

            migrationBuilder.CreateIndex(
                name: "IX_OKRActivities_OwnerId",
                table: "OKRActivities",
                column: "OwnerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PinnedRecords_SubProcessId",
                table: "PinnedRecords",
                column: "SubProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ProjectId",
                table: "ProjectActivities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_UserId",
                table: "ProjectActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectId",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedEvents_PublishedById",
                table: "PublishedEvents",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogSummaries_UserId",
                table: "TimeLogSummaries",
                column: "UserId");
        }
    }
}
