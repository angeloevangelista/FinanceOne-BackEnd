using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class addingfinancialmovementsrelationshiptocategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "financial_movements",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reference_date",
                table: "capital_amounts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_financial_movements_CategoryId1",
                table: "financial_movements",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_movements_categories_CategoryId1",
                table: "financial_movements",
                column: "CategoryId1",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_movements_categories_CategoryId1",
                table: "financial_movements");

            migrationBuilder.DropIndex(
                name: "IX_financial_movements_CategoryId1",
                table: "financial_movements");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "financial_movements");

            migrationBuilder.DropColumn(
                name: "reference_date",
                table: "capital_amounts");
        }
    }
}
