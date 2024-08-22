using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChartCareMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pricing_Plan",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pricing_Plan",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pricing_Plan",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "PlanName",
                table: "Pricing_Plan");

            migrationBuilder.DropColumn(
                name: "PlanNameString",
                table: "Pricing_Plan");

            migrationBuilder.DropColumn(
                name: "PlanPrice",
                table: "Pricing_Plan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanName",
                table: "Pricing_Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlanNameString",
                table: "Pricing_Plan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "PlanPrice",
                table: "Pricing_Plan",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "Pricing_Plan",
                columns: new[] { "ID", "PlanName", "PlanNameString", "PlanPrice" },
                values: new object[,]
                {
                    { 1, 0, "Free", 9.99f },
                    { 2, 1, "Standard", 19.99f },
                    { 3, 2, "Premium", 29.99f }
                });
        }
    }
}
