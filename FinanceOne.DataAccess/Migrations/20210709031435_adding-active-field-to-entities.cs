using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class addingactivefieldtoentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "active",
                table: "users",
                type: "char",
                nullable: false,
                defaultValue: "Y");

            migrationBuilder.AddColumn<string>(
                name: "active",
                table: "refresh_tokens",
                type: "char",
                nullable: false,
                defaultValue: "Y");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "users");

            migrationBuilder.DropColumn(
                name: "active",
                table: "refresh_tokens");
        }
    }
}
