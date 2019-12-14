using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stepeco.Migrations
{
    public partial class recommendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Minimum = table.Column<double>(nullable: true),
                    Maximum = table.Column<double>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recommendations");
        }
    }
}
