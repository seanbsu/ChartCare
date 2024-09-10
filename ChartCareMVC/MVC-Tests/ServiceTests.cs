using ChartCareMVC.Services.FeaturesService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Tests
{
    public class ServiceTests : TestHelpers
    {

        [Fact]
        public async Task TestGetFeaturesNotNullAync()
        {
            // Arrange
            var serviceProvider = ConfigureServices();

            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProviderInScope = scope.ServiceProvider;
                try
                {
                    SeedDatabase(serviceProviderInScope);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Assert.Fail("failed to seed Database");
                }

                var featureService = serviceProviderInScope.GetRequiredService<IFeatureService>();

                // Act
                var result = await featureService.GetAllUniqueFeatures();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);
            }
        }

        [Fact]
        public async Task TestGetFeaturesNullAync()
        {
            // Arrange
            var serviceProvider = ConfigureServices();

            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProviderInScope = scope.ServiceProvider;
                

                var featureService = serviceProviderInScope.GetRequiredService<IFeatureService>();

                // Act
                var result = await featureService.GetAllUniqueFeatures();

                // Assert
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Null(result.Data);
            }
        }

    }
}
