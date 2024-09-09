using Xunit;
using ChartCareMVC;
using Microsoft.AspNetCore.Mvc.Testing;
namespace MVC_Tests
{
    public class WebPageTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;//useful for when factory client isn't reading the page well

        public WebPageTests() {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = new HttpClient();
        }

        [Fact(Skip = "moved to another test")]
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

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Features")]
        [InlineData("/Home/Pricing")]
        [InlineData("/Identity/Account/Register")]
        [InlineData("/Identity/Account/Login")]
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

        [Fact]
        public async void TestRegisterPageContents()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Identity/Account/Register");
            response.EnsureSuccessStatusCode(); 

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Company Name", responseString);
            Assert.Contains("Email", responseString);
            Assert.Contains("Password", responseString);
            Assert.Contains("Confirm Password", responseString);
            Assert.Contains("Pricing Plan", responseString);
            Assert.Contains("Address", responseString);
            Assert.Contains("Free", responseString);
            Assert.Contains("Standard", responseString);
            Assert.Contains("Premium", responseString);
        }

        [Fact]
        public async void TestLoginPageContents()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Identity/Account/Login");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            
            Assert.Contains("Email", responseString);
            Assert.Contains("Password", responseString);
            Assert.Contains("Remember me?", responseString);
            Assert.Contains("Login", responseString);
            Assert.Contains("Register as a new user", responseString);
            Assert.Contains("Forgot your password?", responseString);
            Assert.Contains("Resend email confirmation", responseString);
        }

    }
    
}