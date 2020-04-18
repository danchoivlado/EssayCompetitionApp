using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class AddedContestantContest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContestantContest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ContestantId = table.Column<string>(nullable: true),
                    ContestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestantContest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContestantContest_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestantContest_AspNetUsers_ContestantId",
                        column: x => x.ContestantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestantContest_ContestId",
                table: "ContestantContest",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestantContest_ContestantId",
                table: "ContestantContest",
                column: "ContestantId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestantContest_IsDeleted",
                table: "ContestantContest",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestantContest");
        }
    }
}
