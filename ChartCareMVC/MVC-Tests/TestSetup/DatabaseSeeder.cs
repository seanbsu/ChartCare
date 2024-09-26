using ChartCareData.Identity.Data;
using ChartCareData.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Tests.TestSetup
{
    internal class DatabaseSeeder
    {
        public void SeedDatabase(CompanyDbContext context)
        {
                context.Database.EnsureCreated();
           
                SeedPricingPlans(context);
                SeedFeatures(context);
                SeedPlanFeatures(context);
                context.SaveChanges();
        }

        private void SeedPricingPlans (CompanyDbContext context)
        {

            if (!context.PricingPlans.Any())
            {
                context.PricingPlans.AddRange(
                new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
                new PricingPlan { ID = 2, PlanName = Plan.Standard, PlanNameString = "Standard", PlanPrice = 19.99f },
                new PricingPlan { ID = 3, PlanName = Plan.Premium, PlanNameString = "Premium", PlanPrice = 29.99f }
                );
            }
        }

        private void SeedFeatures(CompanyDbContext context)
        {
            if (!context.Features.Any())
            {
                context.Features.AddRange(
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
            }
        }

        private void SeedPlanFeatures(CompanyDbContext context)
        {
            if (!context.PlanFeatures.Any())
            {
                context.PlanFeatures.AddRange(
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


    }
}
