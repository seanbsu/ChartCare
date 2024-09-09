using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChartCareMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddFeaturesAbrevDescColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AbbreviatedDescription",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 1,
                column: "AbbreviatedDescription",
                value: "Create up to 50 employee accounts");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 2,
                column: "AbbreviatedDescription",
                value: "Create up to 500 employee accounts");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 3,
                column: "AbbreviatedDescription",
                value: "No limit on employee accounts created");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 4,
                column: "AbbreviatedDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 5,
                column: "AbbreviatedDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 6,
                column: "AbbreviatedDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 7,
                column: "AbbreviatedDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 8,
                column: "AbbreviatedDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "ID",
                keyValue: 9,
                column: "AbbreviatedDescription",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbbreviatedDescription",
                table: "Features");
        }
    }
}
