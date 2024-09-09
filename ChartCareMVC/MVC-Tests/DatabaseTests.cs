using Microsoft.EntityFrameworkCore;
using ChartCareMVC.Areas.Identity.Data;
using ChartCareMVC.Data;
using ChartCareMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using ChartCareMVC.Services.PricingPlanService;
using Microsoft.Extensions.DependencyInjection;


namespace MVC_Tests
{
    public class DatabaseTests : TestHelpers
    {

        [Fact]
        public void TestAddCompany()
        {
            // Arrange
            var serviceProvider = ConfigureServices();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

                // Arrange
                var company = new Company
                {
                    Name = "Test Company",
                    Address = "123 Test St",
                    PlanID = 1,
                    Email = "test@company.com",
                    PricingPlan = new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
                    CompanyUsers = new List<CompanyUser>()
                };

                // Act
                context.Companies.Add(company);
                context.SaveChanges();

                // Assert
                var addedCompany = context.Companies.FirstOrDefault(c => c.ID == company.ID);
                Assert.NotNull(addedCompany);
                Assert.Equal("Test Company", addedCompany.Name);
                Assert.Equal("123 Test St", addedCompany.Address);
                Assert.Equal("test@company.com", addedCompany.Email);

                // Cleanup
                context.Companies.Remove(addedCompany);
                context.SaveChanges();
            }
        }


        [Fact]
        public void CompletelyAddCompany()
        {
            // Arrange
            var serviceProvider = ConfigureServices();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

                var company = new Company
                {
                    Name = "Test Company",
                    Address = "123 Test St",
                    PlanID = 1,
                    Email = "test@company.com",
                    PricingPlan = new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
                    CompanyUsers = new List<CompanyUser>()
                };

                var companyAdmin = new CompanyUser
                {
                    FirstName = "Test",
                    LastName = "Admin",
                    Email = "test@company.com",
                    Company = company
                };

                company.CompanyUsers.Add(companyAdmin);

                // Act
                context.Companies.Add(company);
                context.SaveChanges();

                // Assert
                var addedCompany = context.Companies.Include(c => c.CompanyUsers).FirstOrDefault(c => c.ID == company.ID);
                Assert.NotNull(addedCompany);
                Assert.Equal("Test Company", addedCompany.Name);
                Assert.Equal("123 Test St", addedCompany.Address);
                Assert.Equal("test@company.com", addedCompany.Email);

                Assert.Single(addedCompany.CompanyUsers);
                var addedCompanyAdmin = addedCompany.CompanyUsers.First();
                Assert.NotNull(addedCompanyAdmin);
                Assert.Equal("Test", addedCompanyAdmin.FirstName);
                Assert.Equal("Admin", addedCompanyAdmin.LastName);
                Assert.Equal("test@company.com", addedCompanyAdmin.Email);
                Assert.Equal(addedCompany.ID, addedCompanyAdmin.CompanyID);

                
                context.Companies.Remove(addedCompany);
                context.SaveChanges();
            }
        }

        [Fact]
        public void AddCompanyWithMultipleUsers()
        {
            // Arrange
            var serviceProvider = ConfigureServices();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

                var company = new Company
                {
                    Name = "Test Company",
                    Address = "123 Test St",
                    PlanID = 1,
                    Email = "test@company.com",
                    PricingPlan = new PricingPlan { ID = 1, PlanName = Plan.Free, PlanNameString = "Free", PlanPrice = 0.00f },
                    CompanyUsers = new List<CompanyUser>()
                };

                var companyAdmin = new CompanyUser
                {
                    FirstName = "Test",
                    LastName = "Admin",
                    Email = "test@company.com",
                    Company = company
                };
                company.CompanyUsers.Add(companyAdmin);

                var companyUser = new CompanyUser
                {
                    FirstName = "Adam",
                    LastName = "Sandler",
                    Email = "adamsandler@company.com",
                    Company = company
                };

                company.CompanyUsers.Add(companyUser);

                // Act
                context.Companies.Add(company);
                context.SaveChanges();

                // Assert
                var addedCompany = context.Companies
                    .Include(c => c.CompanyUsers)
                    .FirstOrDefault(c => c.ID == company.ID);

                Assert.NotNull(addedCompany);
                Assert.Equal(2, addedCompany.CompanyUsers.Count);
                Assert.Contains(addedCompany.CompanyUsers, u => u.Email == "adamsandler@company.com");
                Assert.Contains(addedCompany.CompanyUsers, u => u.Email == "test@company.com");

                // Cleanup
                context.Companies.Remove(addedCompany);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestGetPricingPlansAsync()
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
                    Assert.Fail(e.ToString());
                }

                var pricingPlanService = serviceProviderInScope.GetRequiredService<IPricingPlanService>();

                // Act
                var result = await pricingPlanService.GetPricingPlansAsync();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);
                Assert.Equal(3, result.Data.Count);
            }
        }

        [Fact]
        public async Task TestGetPricingPlansByIDAsync()
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
                    Assert.Fail(e.ToString());
                }

                var pricingPlanService = serviceProviderInScope.GetRequiredService<IPricingPlanService>();

                // Act
                var result = await pricingPlanService.GetPricingPlanByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);
                Assert.Equal("Free", result.Data.PlanNameString);
            }
        }

        [Fact]
        public async Task TestGetPlanFeatures()
        {
            //Arrange
            var serviceProvider = ConfigureServices();
            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProviderInScope = scope.ServiceProvider;
                try 
                {
                    SeedDatabase(serviceProviderInScope); 
                } catch(Exception e) 
                {
                    Assert.Fail(e.ToString());
                }
                
                var context = serviceProviderInScope.GetService<CompanyDbContext>();
                if (context == null)
                {
                    Assert.Fail("DB context was null check configuration in ConfigureServices helper");
                }
                var pricingPlanService = serviceProviderInScope.GetRequiredService<IPricingPlanService>();
                //Act
                var result = await pricingPlanService.GetPlanFeaturesAsync("Free");
                var expectedResult = context.PricingPlans
                                    .Where(pp => pp.PlanNameString == "Free")
                                    .GroupJoin(context.PlanFeatures,
                                        pp => pp.ID,
                                        pf => pf.PlanId,
                                        (pp, planFeaturesGroup) => new { pp, planFeaturesGroup })
                                    .SelectMany(
                                        x => x.planFeaturesGroup.DefaultIfEmpty(),
                                        (x, planFeatures) => new { x.pp, planFeatures })
                                    .GroupJoin(context.Features,
                                        pf => pf.planFeatures != null ? pf.planFeatures.FeatureId : 0,
                                        f => f.ID,
                                        (pf, featuresGroup) => new { pf.pp, pf.planFeatures, featuresGroup })
                                    .SelectMany(
                                        x => x.featuresGroup.DefaultIfEmpty(),
                                        (x, feature) => new
                                        {
                                            PlanNameString = x.pp.PlanNameString,
                                            PlanPrice = x.pp.PlanPrice,
                                            FeatureName = feature != null ? feature.Name : null,
                                            FeatureDescription = feature != null ? feature.Description : null
                                        })
                                    .OrderBy(x => x.PlanNameString)
                                    .ToList();

                //Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);
                Assert.Equal(expectedResult.Count, result.Data.Count);
                for (int i = 0; i < expectedResult.Count; i++)
                {
                    var expected = expectedResult[i];
                    var actual = result.Data[i];

                    Assert.Equal(expected.FeatureName, actual.Name);
                    Assert.Equal(expected.FeatureDescription, actual.Description);
                }

            }
        }

        [Fact]
        public async Task TestGetAllPlansWithFeaturesAsync()
        {
            //Arrange
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
                    Assert.Fail(e.ToString());
                }

                var pricingPlanService = serviceProviderInScope.GetRequiredService<IPricingPlanService>();
                //Act
                var result = await pricingPlanService.GetAllPlansWithFeaturesAsync();
                //Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);

            }
        }

        [Fact]
        public async Task TestGetCascadedPlansWithFeatures()
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
                    Assert.Fail(e.ToString());
                }

                var pricingPlanService = serviceProviderInScope.GetRequiredService<IPricingPlanService>();

                // Act
                var plansWithFeatures = await pricingPlanService.GetAllPlansWithFeaturesAsync();
                if (plansWithFeatures.Data == null)
                {
                    Assert.Fail("Error with GetAllPlansWithFeaturesAsync. Consider adding more tests for that method");
                }

                var result = pricingPlanService.GetCascadedPlansWithFeatures(plansWithFeatures.Data);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Data);

                var context = serviceProviderInScope.GetRequiredService<CompanyDbContext>();
                var allFeatures = context.Features.ToList();

                var expected = context.PricingPlans
                    .Include(pp => pp.PlanFeatureLinks)
                        .ThenInclude(pf => pf.Feature)
                    .OrderBy(pp => pp.ID)
                    .ToDictionary(
                        pp => pp,
                        pp => pp.PlanFeatureLinks
                            .Select(pf => pf.Feature)
                            .ToList()
                    );

                var cascadedExpected = new Dictionary<PricingPlan, List<Features>>();
                foreach (var plan in expected.Keys.OrderBy(p => p.ID))
                {
                    var features = new List<Features>();
                    foreach (var kvp in expected)
                    {
                        if (kvp.Key.ID <= plan.ID)
                        {
                            var filteredFeatures = kvp.Value
                                .Where(f => !f.Description.Contains("employee accounts"))
                                .ToList();
                            features.AddRange(filteredFeatures);
                        }
                    }

                    var uniqueFeatures = expected[plan]
                        .Where(f => f.Description.Contains("employee accounts"))
                        .ToList();
                    features.AddRange(uniqueFeatures);

                    features = features.Distinct().ToList();
                    cascadedExpected[plan] = features;
                }

                Assert.Equal(cascadedExpected.Count, result.Data.Count);
                foreach (var kvp in cascadedExpected)
                {
                    var actualList = result.Data[kvp.Key];
                    var expectedList = kvp.Value;

                    Assert.Equal(expectedList.Count, actualList.Count);

                    foreach (var expectedFeature in expectedList)
                    {
                        Assert.Contains(expectedFeature, actualList);
                    }
                }
            }
        }




    }
}
