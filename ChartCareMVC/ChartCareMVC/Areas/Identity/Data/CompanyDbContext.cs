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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Company>().ToTable("Company");
        builder.Entity<PricingPlan>().ToTable("Pricing_Plan");

        builder.Entity<CompanyUser>()
        .HasOne(u => u.Company)
        .WithMany(c => c.CompanyUsers)
        .HasForeignKey(u => u.CompanyID) // Make sure this matches the column name in AspNetUsers
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PricingPlan>()
        .Property(p => p.PlanName)
        .HasConversion(
            v => (int)v, 
            v => (Plan)v 
        );

        builder.Entity<PricingPlan>().HasData(
        new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
        new PricingPlan { ID = 2, PlanName = Plan.Standard, PlanNameString = "Standard", PlanPrice = 19.99f },
        new PricingPlan { ID = 3, PlanName = Plan.Premium, PlanNameString = "Premium", PlanPrice = 29.99f }
        );
    }
}
