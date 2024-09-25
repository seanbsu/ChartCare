using Microsoft.EntityFrameworkCore;
using ChartCareData.Identity.Data;
using ChartCareData.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using ChartCareData.Services.PricingPlanService;
using ChartCareData.Services.FeaturesService;
using Microsoft.Extensions.DependencyInjection;
using MVC_Tests.TestSetup;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MVC_Tests
{
    public class TestHelpers
    {
        private readonly DatabaseSeeder _databaseSeeder = new DatabaseSeeder();
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Register DbContext with an in-memory database
            serviceCollection.AddDbContext<CompanyDbContext>(opts =>
                opts.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),ServiceLifetime.Transient);

            // Register services
            serviceCollection.AddScoped<IPricingPlanService, PricingPlanService>();
            serviceCollection.AddScoped<IFeatureService, FeatureService>();

            // Build the service provider
            return serviceCollection.BuildServiceProvider();
        }

        public void SeedDatabase(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<CompanyDbContext>())
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                _databaseSeeder.SeedDatabase(context);
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
