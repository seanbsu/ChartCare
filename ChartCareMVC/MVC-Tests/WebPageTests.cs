using Xunit;
using ChartCareMVC;
using Microsoft.AspNetCore.Mvc.Testing;
namespace MVC_Tests
{
    public class WebPageTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;
        private readonly ExpectedPageContent _expectedPageContent;

        public WebPageTests() {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
            httpClient = new HttpClient();
            _expectedPageContent = new ExpectedPageContent();
        }


        [Fact]
        public async void TestNavBarPresence()
        {
            //Arrange
            var client = _factory.CreateClient();
            List<string> expectedContents = _expectedPageContent.ExpectedNavBarElements();

            //Act
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode(); // Ensure the page loaded successfully

            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            foreach (string content in expectedContents)
            {
                Assert.Contains(content, responseString);
            }

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
            List<string> expectedContents = _expectedPageContent.ExpectedRegistrationElements();

            var response = await client.GetAsync("/Identity/Account/Register");
            response.EnsureSuccessStatusCode(); 

            var responseString = await response.Content.ReadAsStringAsync();

            foreach (string content in expectedContents)
            {
                Assert.Contains(content, responseString);
            }
        }

        [Fact]
        public async void TestLoginPageContents()
        {
            var client = _factory.CreateClient();
            List<string> expectedContents = _expectedPageContent.ExpectedLoginElements();

            var response = await client.GetAsync("/Identity/Account/Login");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            foreach (string content in expectedContents)
            {
                Assert.Contains(content, responseString);
            }
        }

    }
    
}