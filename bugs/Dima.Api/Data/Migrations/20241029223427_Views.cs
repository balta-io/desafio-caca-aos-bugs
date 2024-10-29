using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class Views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW [vwGetExpensesByCategory] AS
                    SELECT
                        [Transaction].[UserId],
                        [Category].[Title] AS [Category],
                        YEAR([Transaction].[PaidOrReceivedAt]) AS [Year],
                        SUM([Transaction].[Amount]) AS [Expenses]
                    FROM
                        [Transaction]
                            INNER JOIN [Category]
                                       ON [Transaction].[CategoryId] = [Category].[Id]
                    WHERE
                        [Transaction].[PaidOrReceivedAt]
                            >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
                      AND [Transaction].[PaidOrReceivedAt]
                        < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
                      AND [Transaction].[Type] = 2
                    GROUP BY
                        [Transaction].[UserId],
                        [Category].[Title],
                        YEAR([Transaction].[PaidOrReceivedAt])
              
             ");

            migrationBuilder.Sql
            (@"
  
                CREATE OR ALTER VIEW [vwGetIncomesAndExpenses] AS
                    SELECT
                        [Transaction].[UserId],
                        MONTH([Transaction].[PaidOrReceivedAt]) AS [Month],
                        YEAR([Transaction].[PaidOrReceivedAt]) AS [Year],
                        SUM(CASE WHEN [Transaction].[Type] = 1 THEN [Transaction].[Amount] ELSE 0 END) AS [Incomes],
                        SUM(CASE WHEN [Transaction].[Type] = 2 THEN [Transaction].[Amount] ELSE 0 END) AS [Expenses]
                    FROM
                        [Transaction]
                    WHERE
                        [Transaction].[PaidOrReceivedAt]
                            >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
                      AND [Transaction].[PaidOrReceivedAt]
                        < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
                    GROUP BY
                        [Transaction].[UserId],
                        MONTH([Transaction].[PaidOrReceivedAt]),
                        YEAR([Transaction].[PaidOrReceivedAt]);
            ");

            migrationBuilder.Sql
            (@"
                CREATE OR ALTER VIEW [vwGetIncomesByCategory] AS
                    SELECT
                        [Transaction].[UserId],
                        [Category].[Title] AS [Category],
                        YEAR([Transaction].[PaidOrReceivedAt]) AS [Year],
                        SUM([Transaction].[Amount]) AS [Incomes]
                    FROM
                        [Transaction]
                            INNER JOIN [Category]
                                       ON [Transaction].[CategoryId] = [Category].[Id]
                    WHERE
                        [Transaction].[PaidOrReceivedAt]
                            >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
                      AND [Transaction].[PaidOrReceivedAt]
                        < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
                      AND [Transaction].[Type] = 1
                    GROUP BY
                        [Transaction].[UserId],
                        [Category].[Title],
                        YEAR([Transaction].[PaidOrReceivedAt])
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP ViEW vwGetExpensesByCategory;
                DROP ViEW vwGetIncomesAndExpenses;
                DROP ViEW vwGetIncomesByCategory;
                ");
        }
    }
}
