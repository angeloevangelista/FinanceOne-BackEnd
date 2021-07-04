using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class creatingusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    first_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    last_name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    password = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
