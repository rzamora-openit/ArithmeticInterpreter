﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddWFHInRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWFH",
                table: "Records",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWFH",
                table: "Records");
        }
    }
}
