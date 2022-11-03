using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class updateOKRActivty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "OKRActivities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "OKRActivities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "OKRActivities");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "OKRActivities");
        }
    }
}
