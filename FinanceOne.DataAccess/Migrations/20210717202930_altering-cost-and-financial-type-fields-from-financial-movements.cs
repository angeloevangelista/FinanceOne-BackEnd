using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceOne.DataAccess.Migrations
{
    public partial class alteringcostandfinancialtypefieldsfromfinancialmovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "financial_movements",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "FinancialMovementType",
                table: "financial_movements",
                newName: "financial_movement_type");

            migrationBuilder.AlterColumn<string>(
                name: "financial_movement_type",
                table: "financial_movements",
                type: "char",
                nullable: false,
                defaultValue: "E",
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cost",
                table: "financial_movements",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "financial_movement_type",
                table: "financial_movements",
                newName: "FinancialMovementType");

            migrationBuilder.AlterColumn<int>(
                name: "FinancialMovementType",
                table: "financial_movements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldDefaultValue: "E");
        }
    }
}
