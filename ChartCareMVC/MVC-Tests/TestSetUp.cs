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
    public class TestSetUp
    {
        private readonly DatabaseSeeder _databaseSeeder = new DatabaseSeeder();
        private readonly ServicesConfigSetup _servicesConfigurer = new ServicesConfigSetup();
        public IServiceProvider ConfigureServices()
        {
            return _servicesConfigurer.ConfigureServices();
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
