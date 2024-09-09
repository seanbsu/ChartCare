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
                SeedDatabase(serviceProviderInScope);

                var featureService = serviceProviderInScope.GetRequiredService<IFeatureService>();

                // Act
                var result = await featureService.GetAllUniqueFeatures();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);
            }
        }


    }
}
