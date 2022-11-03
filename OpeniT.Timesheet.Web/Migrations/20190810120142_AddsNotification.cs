using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ForUserId = table.Column<int>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    OriginType = table.Column<string>(nullable: true),
                    OriginIdentifier = table.Column<int>(nullable: false),
                    OriginCreated = table.Column<DateTimeOffset>(nullable: false),
                    OriginUserId = table.Column<int>(nullable: true),
                    DateIssued = table.Column<DateTimeOffset>(nullable: false),
                    Notified = table.Column<bool>(nullable: false),
                    DateNotified = table.Column<DateTimeOffset>(nullable: false),
                    Seen = table.Column<bool>(nullable: false),
                    DateSeen = table.Column<DateTimeOffset>(nullable: false),
                    Importance = table.Column<int>(nullable: false),
                    Superseded = table.Column<bool>(nullable: false),
                    SupersededById = table.Column<int>(nullable: true),
                    RelevantInfo = table.Column<string>(nullable: true),
                    Param1 = table.Column<string>(nullable: true),
                    Param2 = table.Column<string>(nullable: true),
                    Param3 = table.Column<string>(nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
