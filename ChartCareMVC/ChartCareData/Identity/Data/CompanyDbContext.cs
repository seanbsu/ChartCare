
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChartCareData.Models;

namespace ChartCareData.Identity.Data;

public class CompanyDbContext : IdentityDbContext<CompanyUser>
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<PricingPlan> PricingPlans { get; set; }
    public DbSet<Features> Features { get; set; }  
    public DbSet<PlanFeatures> PlanFeatures { get; set; } 

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Company>().ToTable("Company");
        builder.Entity<PricingPlan>().ToTable("Pricing_Plan");
        builder.Entity<Features>().ToTable("Features");
        builder.Entity<PlanFeatures>().ToTable("PlanFeatures");

        DefineTableRelationships(builder);

        SeedPlans(builder);

        SeedFeatures(builder);
    }

    /// <summary>
    /// Configures the relationships between the tables in the database.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> used to configure the relationships.</param>
    private void DefineTableRelationships(ModelBuilder builder)
    {
        builder.Entity<CompanyUser>()
          .HasOne(companyUser => companyUser.Company)
          .WithMany(company => company.CompanyUsers)
          .HasForeignKey(companyUser => companyUser.CompanyID)
          .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PlanFeatures>()
            .HasKey(planFeature => new { planFeature.PlanId, planFeature.FeatureId });

        builder.Entity<PlanFeatures>()
            .HasOne(planFeature => planFeature.PricingPlan)
            .WithMany(pricingPlan => pricingPlan.PlanFeatureLinks)
            .HasForeignKey(planFeature => planFeature.PlanId);

        builder.Entity<PlanFeatures>()
            .HasOne(planFeature => planFeature.Feature)
            .WithMany(feature => feature.PlanFeatureLinks)
            .HasForeignKey(planFeature => planFeature.FeatureId);

        builder.Entity<PricingPlan>()
            .Property(pricingPlan => pricingPlan.PlanName)
            .HasConversion(
                plan => (int)plan,
                value => (Plan)value
            );
    }

    /// <summary>
    /// Seeds the database with initial pricing plans.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> used to seed the data.</param>
    private void SeedPlans(ModelBuilder builder)
    {
       builder.Entity<PricingPlan>().HasData(
       new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
       new PricingPlan { ID = 2, PlanName = Plan.Standard, PlanNameString = "Standard", PlanPrice = 19.99f },
       new PricingPlan { ID = 3, PlanName = Plan.Premium, PlanNameString = "Premium", PlanPrice = 29.99f }
       );
    }

    /// <summary>
    /// Seeds the database with initial feature data and links them to pricing plans.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> used to seed the data.</param>
    private void SeedFeatures(ModelBuilder builder)
    {
        builder.Entity<Features>().HasData(
            new Features
            {
                ID = 1,
                Name = "Employee Count Free",
                Description = "Create up to 50 employee accounts for your organization",
                AbbreviatedDescription = "Create up to 50 employee accounts"
            },
            new Features
            {
                ID = 2,
                Name = "Employee Count Standard",
                Description = "Create up to 500 employee accounts for your organization",
                AbbreviatedDescription = "Create up to 500 employee accounts"
            },
            new Features
            {
                ID = 3,
                Name = "Employee Count Premium",
                Description = "No limit on employee accounts created for your organization",
                AbbreviatedDescription = "No limit on employee accounts created"
            },
            new Features
            {
                ID = 4,
                Name =
                "Real Time Charting",
                Description = "Instantly updates patient charts with live data, ensuring that any modifications or new entries are reflected across all connected devices without delay. "
            },
            new Features
            {
                ID = 5,
                Name = "Patient Workload Customization",
                Description = "Ability to create a personalized list of patients for each company user"
            },
            new Features
            {
                ID = 6,
                Name = "Basic Analytics",
                Description = "Provides essential insights into patient data through simple, easy-to-understand visualizations and reports. Metrics include:" +
                  " Patient Visit Frequency," +
                  " Patient Demographics," +
                  " Treatment Success Rates," +
                  " Average Length of Stay," +
                  " Medication Adherence,"
            },
             new Features
             {
                 ID = 7,
                 Name = "Advanced Analytics",
                 Description = "Provides additional insights into patient data through simple, easy-to-understand visualizations and reports. Metrics include:" +
                  " Cost-Per-Treatment Analysis," +
                  " Longitudinal Patient Progress Tracking," +
                  " Readmission Rates," +
                  " Patient Satisfaction Scores"
             },
             new Features
             {
                 ID = 8,
                 Name = "Automated Notifications",
                 Description = "Automatically sends alerts and reminders to users about important events, such as upcoming appointments, medication schedules, and critical patient status changes."
             },
             new Features
             {
                 ID = 9,
                 Name = "Priority Support",
                 Description = "Gives access to priority customer support with faster response times, ensuring any issues or inquiries are addressed promptly."
             }
        ); 
        builder.Entity<PlanFeatures>().HasData(
            new PlanFeatures { FeatureId = 1, PlanId = 1 },
            new PlanFeatures { FeatureId = 4, PlanId = 1 },
            new PlanFeatures { FeatureId = 5, PlanId = 1 },
            new PlanFeatures { FeatureId = 2, PlanId = 2 },
            new PlanFeatures { FeatureId = 6, PlanId = 2 },
            new PlanFeatures { FeatureId = 8, PlanId = 2 },
            new PlanFeatures { FeatureId = 3, PlanId = 3 },
            new PlanFeatures { FeatureId = 7, PlanId = 3 },
            new PlanFeatures { FeatureId = 9, PlanId = 3 }
            );
    }
}
