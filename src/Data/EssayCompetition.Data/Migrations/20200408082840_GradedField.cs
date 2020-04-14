namespace EssayCompetition.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class GradedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Graded",
                table: "Essays",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Graded",
                table: "Essays");
        }
    }
}
