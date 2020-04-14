using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class AddContest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_AspNetUsers_TeacherId",
                table: "Essays");

            migrationBuilder.DropIndex(
                name: "IX_Essays_TeacherId",
                table: "Essays");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Essays");

            migrationBuilder.AddColumn<int>(
                name: "ContestId",
                table: "Essays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contests_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Essays_ContestId",
                table: "Essays",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Contests_CategoryId",
                table: "Contests",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contests_IsDeleted",
                table: "Contests",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_Contests_ContestId",
                table: "Essays",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_Contests_ContestId",
                table: "Essays");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropIndex(
                name: "IX_Essays_ContestId",
                table: "Essays");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "Essays");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Essays",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Essays_TeacherId",
                table: "Essays",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_AspNetUsers_TeacherId",
                table: "Essays",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
