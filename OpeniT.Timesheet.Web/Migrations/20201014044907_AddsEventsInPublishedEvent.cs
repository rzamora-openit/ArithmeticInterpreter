using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsEventsInPublishedEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublishedEventId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_PublishedEventId",
                table: "Events",
                column: "PublishedEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_PublishedEvents_PublishedEventId",
                table: "Events",
                column: "PublishedEventId",
                principalTable: "PublishedEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_PublishedEvents_PublishedEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PublishedEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PublishedEventId",
                table: "Events");
        }
    }
}
