using Xunit;
using ChartCareMVC;
using Microsoft.AspNetCore.Mvc.Testing;
namespace MVC_Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;//useful for when factory client is reading the page well

        public UnitTest1() {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = new HttpClient();
        }

        [Fact(Skip ="moved to other test")]
        public async void TestHomeLoads()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync("/");
            int code = (int)response.StatusCode;
            //Assert
            Assert.Equal(200, code);

        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        public async void TestAllPagesLoad(string URL)
        {
            //Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;
            //Assert
            Assert.Equal(200, code);
        }
    }
}