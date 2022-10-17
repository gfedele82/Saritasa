using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RUB = table.Column<double>(type: "Float", nullable: false),
                    EUR = table.Column<double>(type: "Float", nullable: false),
                    GBP = table.Column<double>(type: "Float", nullable: false),
                    JPY = table.Column<double>(type: "Float", nullable: false),
                    Response = table.Column<string>(type: "Text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
