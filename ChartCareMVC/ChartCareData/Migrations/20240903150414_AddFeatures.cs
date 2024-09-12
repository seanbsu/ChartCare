using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChartCareMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Pricing_Plan_PricingPlanID",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_PricingPlanID",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "PricingPlanID",
                table: "Company");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlanFeatures",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanFeatures", x => new { x.PlanId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_PlanFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanFeatures_Pricing_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Pricing_Plan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Create up to 50 employee accounts for your organization", "Employee Count Free" },
                    { 2, "Create up to 500 employee accounts for your organization", "Employee Count Standard" },
                    { 3, "No limit on employee accounts created for your organization", "Employee Count Premium" },
                    { 4, "Instantly updates patient charts with live data, ensuring that any modifications or new entries are reflected across all connected devices without delay. ", "Real Time Charting" },
                    { 5, "Ability to create a personalized list of patients for each company user", "Patient Workload Customization" },
                    { 6, "Provides essential insights into patient data through simple, easy-to-understand visualizations and reports. Metrics include: Patient Visit Frequency, Patient Demographics, Treatment Success Rates, Average Length of Stay, Medication Adherence,", "Basic Analytics" },
                    { 7, "Provides additional insights into patient data through simple, easy-to-understand visualizations and reports. Metrics include: Cost-Per-Treatment Analysis, Longitudinal Patient Progress Tracking, Readmission Rates, Patient Satisfaction Scores", "Advanced Analytics" },
                    { 8, "Automatically sends alerts and reminders to users about important events, such as upcoming appointments, medication schedules, and critical patient status changes.", "Automated Notifications" },
                    { 9, "Gives access to priority customer support with faster response times, ensuring any issues or inquiries are addressed promptly.", "Priority Support" }
                });

            migrationBuilder.InsertData(
                table: "PlanFeatures",
                columns: new[] { "FeatureId", "PlanId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 2, 2 },
                    { 6, 2 },
                    { 8, 2 },
                    { 3, 3 },
                    { 7, 3 },
                    { 9, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_PlanID",
                table: "Company",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanFeatures_FeatureId",
                table: "PlanFeatures",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Pricing_Plan_PlanID",
                table: "Company",
                column: "PlanID",
                principalTable: "Pricing_Plan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Pricing_Plan_PlanID",
                table: "Company");

            migrationBuilder.DropTable(
                name: "PlanFeatures");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Company_PlanID",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "PricingPlanID",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_PricingPlanID",
                table: "Company",
                column: "PricingPlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Pricing_Plan_PricingPlanID",
                table: "Company",
                column: "PricingPlanID",
                principalTable: "Pricing_Plan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
