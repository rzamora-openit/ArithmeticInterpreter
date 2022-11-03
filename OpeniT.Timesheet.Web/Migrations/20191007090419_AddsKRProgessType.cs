using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddsKRProgessType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "KeyResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProgressType",
                table: "KeyResults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "KeyResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TargetPercentage",
                table: "KeyResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TargetQuantity",
                table: "KeyResults",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "ProgressType",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "TargetPercentage",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "TargetQuantity",
                table: "KeyResults");
        }
    }
}
