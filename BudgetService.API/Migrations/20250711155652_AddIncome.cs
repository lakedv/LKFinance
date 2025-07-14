using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetService.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIncome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Budgets");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentDebt",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DebtPaymentPercentage",
                table: "Budgets",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedExpenses",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Budgets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_BudgetId",
                table: "Incomes",
                column: "BudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropColumn(
                name: "CurrentDebt",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DebtPaymentPercentage",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "FixedExpenses",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Budgets");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
