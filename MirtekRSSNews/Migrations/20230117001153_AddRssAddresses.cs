using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MirtekRSSNews.Migrations
{
    public partial class AddRssAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MirtekRSSNews",
                keyColumn: "Id",
                keyValue: new Guid("3c5b38d3-e364-4d46-90fc-6face7ca346c"));

            migrationBuilder.CreateTable(
                name: "UrlRssAdresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlRssAdresses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlRssAdresses");

            migrationBuilder.InsertData(
                table: "MirtekRSSNews",
                columns: new[] { "Id", "DateOfNews", "Text", "Title" },
                values: new object[] { new Guid("3c5b38d3-e364-4d46-90fc-6face7ca346c"), new DateTime(2023, 1, 16, 18, 6, 57, 268, DateTimeKind.Utc).AddTicks(2913), "Some text", "Test News" });
        }
    }
}
