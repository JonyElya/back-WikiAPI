using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Back_Wiki.Migrations
{
    public partial class ArticlesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(nullable: true),
                    snippet = table.Column<string>(nullable: true),
                    pageId = table.Column<int>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "pageId", "snippet", "timestamp", "title" },
                values: new object[] { 1L, 222, "awdasdasdas", new DateTime(2020, 7, 16, 17, 31, 37, 577, DateTimeKind.Utc).AddTicks(7811), "title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
