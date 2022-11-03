using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class UpdatesOKRModelsforCheckin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyResultCheckins");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checkins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectiveId = table.Column<int>(nullable: true),
                    KeyResultId = table.Column<int>(nullable: true),
                    CheckinType = table.Column<string>(nullable: true),
                    CheckinIssueDate = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true),
                    OwnerComment = table.Column<string>(nullable: true),
                    OwnerCommentLocked = table.Column<bool>(nullable: false),
                    OwnerCheckinDate = table.Column<DateTime>(nullable: false),
                    LineManagerId = table.Column<int>(nullable: true),
                    LmComment = table.Column<string>(nullable: true),
                    LmCommentLocked = table.Column<bool>(nullable: false),
                    LmCheckinDate = table.Column<DateTime>(nullable: false),
                    CheckinPercentage = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusLocked = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
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
                name: "CheckinConversation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectiveId = table.Column<int>(nullable: true),
                    KeyResultId = table.Column<int>(nullable: true),
                    CheckinId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(nullable: false),
                    DateEdited = table.Column<DateTimeOffset>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckinConversation");

            migrationBuilder.DropTable(
                name: "Checkins");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KeyResults");

            migrationBuilder.CreateTable(
                name: "KeyResultCheckins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckinDate = table.Column<DateTime>(nullable: false),
                    CheckinPercentage = table.Column<double>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    KeyResultId = table.Column<int>(nullable: true),
                    LastUpdate = table.Column<DateTimeOffset>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ObjectiveId = table.Column<int>(nullable: true),
                    Percent = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResultCheckins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyResultCheckins_KeyResults_KeyResultId",
                        column: x => x.KeyResultId,
                        principalTable: "KeyResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyResultCheckins_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyResultCheckins_KeyResultId",
                table: "KeyResultCheckins",
                column: "KeyResultId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyResultCheckins_ObjectiveId",
                table: "KeyResultCheckins",
                column: "ObjectiveId");
        }
    }
}
