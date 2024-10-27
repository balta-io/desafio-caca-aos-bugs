using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApplySeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var pathScripts = Path.Combine("Data", "Scripts");
            var seedSql  = Path.Combine(pathScripts, "seed.sql");
        
            var sql = File.ReadAllText(seedSql);
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Category];");
            migrationBuilder.Sql("DELETE FROM [Transaction];");
            migrationBuilder.Sql("DELETE FROM [Product];");
        }
    }
}
