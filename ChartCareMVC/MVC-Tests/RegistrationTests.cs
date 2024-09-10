using Microsoft.EntityFrameworkCore;
using ChartCareMVC.Areas.Identity.Data;
using ChartCareMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Identity.UI.Services;
using ChartCareMVC.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;


namespace MVC_Tests
{
    public class RegistrationTests : TestHelpers
    {
        public Mock<UserManager<CompanyUser>> CreateUserManager(Mock<IUserStore<CompanyUser>> userStore, IOptions<IdentityOptions> options)
        {
            var userManager = new Mock<UserManager<CompanyUser>>(
                userStore.Object,
                options,
                Mock.Of<IPasswordHasher<CompanyUser>>(),
                new List<IUserValidator<CompanyUser>>(),
                new List<IPasswordValidator<CompanyUser>>(),
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<CompanyUser>>>());

            // Setting up the UserManager properties and methods
            userManager.Setup(um => um.SupportsUserEmail).Returns(true);

            userManager.Setup(um => um.CreateAsync(It.IsAny<CompanyUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManager.Setup(um => um.GetEmailAsync(It.IsAny<CompanyUser>()))
                .ReturnsAsync((CompanyUser user) => user.Email);

            userManager.Setup(um => um.GetUserIdAsync(It.IsAny<CompanyUser>()))
                .ReturnsAsync((CompanyUser user) => user.Id);

            userManager.Setup(um => um.GenerateEmailConfirmationTokenAsync(It.IsAny<CompanyUser>()))
                .ReturnsAsync("mocked-token");

            userManager.Setup(um => um.GetRolesAsync(It.IsAny<CompanyUser>()))
                .ReturnsAsync(new List<string>());

            // Add other method setups as needed
            return userManager;
        }

        public RegisterModel CreateRegisterModel(
            UserManager<CompanyUser> userManager,
            IUserEmailStore<CompanyUser> emailStore,
            SignInManager<CompanyUser> signInManager,
            IEmailSender emailSender,
            CompanyDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            // Use the custom TestUrlHelper
            var urlHelper = CreateMockUrlHelper();
            urlHelper.Setup(h => h.RouteUrl(It.IsAny<UrlRouteContext>()))
                .Returns("http://localhost:5000/Identity/Account/ConfirmEmail");

            // Create the RegisterModel and assign the mocked dependencies
            var registerModel = new RegisterModel(
                userManager,
                emailStore,
                signInManager,
                Mock.Of<ILogger<RegisterModel>>(),
                emailSender,
                dbContext)
            {
                Input = new RegisterModel.InputModel
                {
                    CompanyName = "Test Company",
                    CompanyAddress = "123 Test St",
                    Email = "test@company.com",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!",
                    PricingPlanID = 1
                },
                Url = urlHelper.Object // Assign the custom IUrlHelper
            };

            // Set the HttpContext
            registerModel.PageContext.HttpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            return registerModel;
        }

        [Fact]
        public async Task TestUnconfirmedAccountRedirect()
        {
            // Arrange
            var serviceProvider = ConfigureServices(); // Configure DI
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                var serviceProviderInScope = scope.ServiceProvider;

                // Seed the database
                try
                {
                    SeedDatabase(serviceProviderInScope);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.ToString());
                }

                // Mock dependencies
                var userStore = new Mock<IUserStore<CompanyUser>>();
                var emailStore = new Mock<IUserEmailStore<CompanyUser>>();
                var emailSender = new Mock<IEmailSender>();

                var httpContextAccessor = new Mock<IHttpContextAccessor>();
                var httpContext = new DefaultHttpContext
                {
                    RequestServices = serviceProvider
                };
                httpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

                var identityOptions = new IdentityOptions
                {
                    SignIn = { RequireConfirmedAccount = true }
                };
                var optionsMock = new Mock<IOptions<IdentityOptions>>();
                optionsMock.Setup(o => o.Value).Returns(identityOptions);

                var userManager = CreateUserManager(userStore, optionsMock.Object);
                var authenticationSchemeProvider = new Mock<IAuthenticationSchemeProvider>();
                var userConfirmation = new Mock<IUserConfirmation<CompanyUser>>();

                var signInManager = new SignInManager<CompanyUser>(
                    userManager.Object,
                    httpContextAccessor.Object,
                    Mock.Of<IUserClaimsPrincipalFactory<CompanyUser>>(),
                    optionsMock.Object,
                    Mock.Of<ILogger<SignInManager<CompanyUser>>>(),
                    authenticationSchemeProvider.Object,
                    userConfirmation.Object);

                var registerModel = CreateRegisterModel(
                    userManager.Object,
                    emailStore.Object,
                    signInManager,
                    emailSender.Object,
                    dbContext,
                    httpContextAccessor.Object);

                // Act
                var result = await registerModel.OnPostAsync("");

                // Assert
                var redirectResult = Assert.IsType<RedirectToPageResult>(result);
                Assert.Equal("RegisterConfirmation", redirectResult.PageName);
                Assert.Null(redirectResult.PageHandler);

                // Verify the user does not have the Admin role yet
                userManager.Verify(um => um.GetRolesAsync(It.IsAny<CompanyUser>()), Times.Never);
            }
        }








    }
}
