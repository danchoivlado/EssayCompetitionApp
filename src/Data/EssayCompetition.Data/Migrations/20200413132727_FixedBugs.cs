namespace EssayCompetition.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_Categories_CategoryId",
                table: "Essays");

            migrationBuilder.DropIndex(
                name: "IX_Essays_CategoryId",
                table: "Essays");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Essays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Essays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Essays_CategoryId",
                table: "Essays",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_Categories_CategoryId",
                table: "Essays",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
