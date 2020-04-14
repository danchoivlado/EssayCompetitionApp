namespace EssayCompetition.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EssayTeacher_AspNetUsers_TeacherId1",
                table: "EssayTeacher");

            migrationBuilder.DropIndex(
                name: "IX_EssayTeacher_TeacherId1",
                table: "EssayTeacher");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "EssayTeacher");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Contests");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "EssayTeacher",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Contests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Contests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_EssayTeacher_TeacherId",
                table: "EssayTeacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_EssayTeacher_AspNetUsers_TeacherId",
                table: "EssayTeacher",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EssayTeacher_AspNetUsers_TeacherId",
                table: "EssayTeacher");

            migrationBuilder.DropIndex(
                name: "IX_EssayTeacher_TeacherId",
                table: "EssayTeacher");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Contests");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "EssayTeacher",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "EssayTeacher",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Contests",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_EssayTeacher_TeacherId1",
                table: "EssayTeacher",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EssayTeacher_AspNetUsers_TeacherId1",
                table: "EssayTeacher",
                column: "TeacherId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
