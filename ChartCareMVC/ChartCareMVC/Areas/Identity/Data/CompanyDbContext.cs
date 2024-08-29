using ChartCareMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChartCareMVC.Models;

namespace ChartCareMVC.Data;

public class CompanyDbContext : IdentityDbContext<CompanyUser>
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<PricingPlan> PricingPlans { get; set; }
    public DbSet<Features> Features { get; set; }  // Added
    public DbSet<PlanFeatures> PlanFeatures { get; set; }  // Added

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

        builder.Entity<PricingPlan>().HasData(
        new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
        new PricingPlan { ID = 2, PlanName = Plan.Standard, PlanNameString = "Standard", PlanPrice = 19.99f },
        new PricingPlan { ID = 3, PlanName = Plan.Premium, PlanNameString = "Premium", PlanPrice = 29.99f }
        );
    }
}
