using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EssayCompetition.Data.Migrations
{
    public partial class AddAdditionaluserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAdditionalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    ConntactPhone = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Social = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdditionalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdditionalInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalInfo_IsDeleted",
                table: "UserAdditionalInfo",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalInfo_UserId",
                table: "UserAdditionalInfo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAdditionalInfo");
        }
    }
}
