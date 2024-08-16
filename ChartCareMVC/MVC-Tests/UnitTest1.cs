using Xunit;
using ChartCareMVC;
using Microsoft.AspNetCore.Mvc.Testing;
namespace MVC_Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;//useful for when factory client isn't reading the page well

        public UnitTest1() {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = new HttpClient();
        }

        [Fact]
        public async void TestHomeLoads()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode(); // Ensure the page loaded successfully

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert that the "Home" link exists and routes to the correct action
            Assert.Contains("href=\"/\"", responseString);
            Assert.Contains(">Home</a>", responseString);

        }
        [Fact]
        public async void TestNavBarPresence()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode(); // Ensure the page loaded successfully

            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Contains("Pricing", responseString);
            Assert.Contains("Features", responseString);
            Assert.Contains("FAQs", responseString);
            Assert.Contains("Home", responseString);
            Assert.Contains("About", responseString);
            Assert.Contains("Login", responseString);
            Assert.Contains("Sign-up", responseString);

        }

        [Theory(Skip = "base links to be supported later")]
        [InlineData("/")]
        [InlineData("/Home/Features")]
        [InlineData("/Home/Pricing")]
        [InlineData("/Home/SignUp")]
        [InlineData("/Home/Login")]
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