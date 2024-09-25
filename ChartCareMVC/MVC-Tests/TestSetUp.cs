using Microsoft.EntityFrameworkCore;
using ChartCareData.Identity.Data;
using ChartCareData.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        private readonly MockConfig _mockConfigurer = new MockConfig();
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

        protected Mock<IUrlHelper> CreateMockUrl(ActionContext? context = null)
        {
            return _mockConfigurer.CreateMockUrlHelper(context);
        }

        



    }
}
