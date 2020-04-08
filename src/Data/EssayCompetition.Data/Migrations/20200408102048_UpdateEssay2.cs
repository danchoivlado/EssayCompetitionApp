using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class UpdateEssay2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Essays",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_AspNetUsers_TeacherId",
                table: "Essays");

            migrationBuilder.DropIndex(
                name: "IX_Essays_TeacherId",
                table: "Essays");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Essays",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
