namespace EssayCompetition.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ModifiedGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_Grades_GradeId",
                table: "Essays");

            migrationBuilder.DropIndex(
                name: "IX_Essays_GradeId",
                table: "Essays");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Essays");

            migrationBuilder.AddColumn<int>(
                name: "EssayId",
                table: "Grades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EssayId",
                table: "Grades",
                column: "EssayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Essays_EssayId",
                table: "Grades",
                column: "EssayId",
                principalTable: "Essays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Essays_EssayId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EssayId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "EssayId",
                table: "Grades");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Essays",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
