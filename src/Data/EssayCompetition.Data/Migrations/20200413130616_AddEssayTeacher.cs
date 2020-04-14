using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class AddEssayTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EssayTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    EssayId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false),
                    TeacherId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EssayTeacher_Essays_EssayId",
                        column: x => x.EssayId,
                        principalTable: "Essays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EssayTeacher_AspNetUsers_TeacherId1",
                        column: x => x.TeacherId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EssayTeacher_EssayId",
                table: "EssayTeacher",
                column: "EssayId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayTeacher_IsDeleted",
                table: "EssayTeacher",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EssayTeacher_TeacherId1",
                table: "EssayTeacher",
                column: "TeacherId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EssayTeacher");
        }
    }
}
