using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterAPI.Migrations
{
    public partial class HistoricalHashtag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricalHashtags",
                columns: table => new
                {
                    HistoricalHashtagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hashtag = table.Column<string>(nullable: true),
                    FromDateTime = table.Column<DateTime>(nullable: true),
                    ToDateTime = table.Column<DateTime>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalHashtags", x => x.HistoricalHashtagID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricalHashtags");
        }
    }
}
