using ChartCareData.Identity.Data;
using ChartCareData.Services.FeaturesService;
using ChartCareData.Services.PricingPlanService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Tests.TestSetup
{
    internal class ServicesConfigSetup
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Register DbContext with an in-memory database
            serviceCollection.AddDbContext<CompanyDbContext>(opts =>
                opts.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()), ServiceLifetime.Transient);

            // Register services
            serviceCollection.AddScoped<IPricingPlanService, PricingPlanService>();
            serviceCollection.AddScoped<IFeatureService, FeatureService>();

            // Build the service provider
            return serviceCollection.BuildServiceProvider();
        }
    }
}
