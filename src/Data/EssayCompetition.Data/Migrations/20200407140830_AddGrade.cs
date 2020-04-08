using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class AddGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Graded",
                table: "Essays");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Essays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    PrivateComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Essays_GradeId",
                table: "Essays",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_Grades_GradeId",
                table: "Essays",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_Grades_GradeId",
                table: "Essays");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Essays_GradeId",
                table: "Essays");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Essays");

            migrationBuilder.AddColumn<bool>(
                name: "Graded",
                table: "Essays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
