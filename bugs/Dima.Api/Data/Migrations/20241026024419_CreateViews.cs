using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {        
            var pathViews = Path.Combine("Data", "Views");
            var vwGetExpensesByCategory = Path.Combine(pathViews, "vwGetExpensesByCategory.sql");
            var vwGetIncomesAndExpenses = Path.Combine(pathViews, "vwGetIncomesAndExpenses.sql");
            var vwGetIncomesByCategory = Path.Combine(pathViews, "vwGetIncomesByCategory.sql");

            var sql = File.ReadAllText(vwGetExpensesByCategory);
            migrationBuilder.Sql(sql);

             sql = File.ReadAllText(vwGetIncomesAndExpenses);
            migrationBuilder.Sql(sql);

             sql = File.ReadAllText(vwGetIncomesByCategory);
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vwGetExpensesByCategory;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS vwGetIncomesAndExpenses;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS vwGetIncomesByCategory;");
        }
    }
}
