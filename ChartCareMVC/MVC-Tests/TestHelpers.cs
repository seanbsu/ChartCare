using Microsoft.EntityFrameworkCore;
using ChartCareMVC.Data;
using ChartCareMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
namespace MVC_Tests
{
    public class TestHelpers
    {
        protected DbContextOptions<CompanyDbContext> CreateNewContextOptions()
        {
            // Create a new instance of the options each time
            return new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        protected void SeedDatabase(DbContextOptions<CompanyDbContext> options)
        {
            using (var context = new CompanyDbContext(options))
            {
                context.Database.EnsureCreated();

                // Check if the PricingPlans table already contains data
                if (!context.PricingPlans.Any())
                {
                    context.PricingPlans.AddRange(
                        new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
                        new PricingPlan { ID = 2, PlanName = Plan.Standard, PlanNameString = "Standard", PlanPrice = 19.99f },
                        new PricingPlan { ID = 3, PlanName = Plan.Premium, PlanNameString = "Premium", PlanPrice = 29.99f }
                    );
                    context.SaveChanges();
                }
            }
        }

        protected Mock<IUrlHelper> CreateMockUrlHelper(ActionContext context = null)
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
