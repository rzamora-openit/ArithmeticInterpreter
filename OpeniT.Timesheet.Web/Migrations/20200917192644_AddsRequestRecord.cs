using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsRequestRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RecordId",
                table: "Requests",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Records_RecordId",
                table: "Requests",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Records_RecordId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RecordId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Requests");
        }
    }
}
