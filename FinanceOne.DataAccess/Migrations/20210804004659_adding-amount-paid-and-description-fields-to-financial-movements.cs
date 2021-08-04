using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class addingamountpaidanddescriptionfieldstofinancialmovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "financial_movements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "amount",
                table: "financial_movements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "paid",
                table: "financial_movements",
                type: "char",
                nullable: false,
                defaultValue: "N");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "financial_movements");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "financial_movements");

            migrationBuilder.DropColumn(
                name: "paid",
                table: "financial_movements");
        }
    }
}
