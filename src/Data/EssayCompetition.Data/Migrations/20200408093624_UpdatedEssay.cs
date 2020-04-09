using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class UpdatedEssay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Essays",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Essays");
        }
    }
}
