using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChartCareMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pricing_Plan",
                keyColumn: "ID",
                keyValue: 1,
                column: "PlanPrice",
                value: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pricing_Plan",
                keyColumn: "ID",
                keyValue: 1,
                column: "PlanPrice",
                value: 9.99f);
        }
    }
}
