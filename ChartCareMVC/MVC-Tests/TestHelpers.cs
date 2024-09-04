using Microsoft.EntityFrameworkCore;
using ChartCareMVC.Data;
using ChartCareMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using ChartCareMVC.Services.PricingPlanService;
using Microsoft.Extensions.DependencyInjection;
namespace MVC_Tests
{
    public class TestHelpers
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Register DbContext with an in-memory database
            serviceCollection.AddDbContext<CompanyDbContext>(opts =>
                opts.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),ServiceLifetime.Transient);

            // Register services
            serviceCollection.AddScoped<IPricingPlanService, PricingPlanService>();

            // Build the service provider
            return serviceCollection.BuildServiceProvider();
        }

        public void SeedDatabase(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<CompanyDbContext>())
            {
                context.Database.EnsureCreated();

                if (!context.PricingPlans.Any())
                {
                    context.PricingPlans.AddRange(
                        new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
                        new PricingPlan { ID = 2, PlanName = Plan.Standard, PlanNameString = "Standard", PlanPrice = 19.99f },
                        new PricingPlan { ID = 3, PlanName = Plan.Premium, PlanNameString = "Premium", PlanPrice = 29.99f }
                    );
                }
                if (!context.Features.Any())
                {
                    context.Features.AddRange(
                        new Features
                        {
                            ID = 1,
                            Name = "Employee Count Free",
                            Description = "Create up to 50 employee accounts for your organization"
                        },
                        new Features
                        {
                            ID = 2,
                            Name = "Employee Count Standard",
                            Description = "Create up to 500 employee accounts for your organization"
                        },
                        new Features
                        {
                            ID = 3,
                            Name = "Employee Count Premium",
                            Description = "No limit on employee accounts created for your organization"
                        }
                    // Add other features as needed
                    );
                }
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
                context.SaveChanges();
            }
        }

        protected Mock<IUrlHelper> CreateMockUrlHelper(ActionContext? context = null)
        {
            context ??= GetActionContextForPage("/Page");

            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupGet(h => h.ActionContext)
                .Returns(context);
            return urlHelper;
        }

        protected static ActionContext GetActionContextForPage(string page)
        {
            return new ActionContext
            {
                ActionDescriptor = new ActionDescriptor
                {
                    RouteValues = new Dictionary<string, string?>
            {
                { "page", page },
            }
                },
                RouteData = new RouteData
                {
                    Values =
            {
                ["page"] = page
            }
                }
            };
        }



    }
}
