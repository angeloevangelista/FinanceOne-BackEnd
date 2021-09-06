using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class addingcapitalamounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "capital_amounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    active = table.Column<string>(type: "char", nullable: false, defaultValue: "Y")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capital_amounts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "capital_amounts");
        }
    }
}
