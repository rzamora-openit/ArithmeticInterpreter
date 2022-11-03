using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpeniT.Timesheet.Web.Migrations
{
    public partial class AddedUserThumbnails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExcessHours_Users_UserId",
                table: "UserExcessHours");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserExcessHours",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserThumbnails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserThumbnails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserThumbnails_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserThumbnails_StudentId",
                table: "UserThumbnails",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExcessHours_Users_UserId",
                table: "UserExcessHours",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExcessHours_Users_UserId",
                table: "UserExcessHours");

            migrationBuilder.DropTable(
                name: "UserThumbnails");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserExcessHours",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_UserExcessHours_Users_UserId",
                table: "UserExcessHours",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
