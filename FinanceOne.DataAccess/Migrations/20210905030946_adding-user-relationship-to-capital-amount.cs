using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class addinguserrelationshiptocapitalamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "capital_amounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_capital_amounts_user_id",
                table: "capital_amounts",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user",
                table: "capital_amounts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user",
                table: "capital_amounts");

            migrationBuilder.DropIndex(
                name: "IX_capital_amounts_user_id",
                table: "capital_amounts");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "capital_amounts");
        }
    }
}
