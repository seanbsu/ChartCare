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


namespace MVC_Tests
{
    public class DatabaseTests : TestHelpers
    {
       





        [Fact]
        public void TestAddCompany()
        {
            var options = CreateNewContextOptions();

            using (var context = new CompanyDbContext(options))
            {
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
            var options = CreateNewContextOptions();

            using (var context = new CompanyDbContext(options))
            {
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

                // Verify the CompanyUser was also added and linked correctly
                Assert.Single(addedCompany.CompanyUsers);
                var addedCompanyAdmin = addedCompany.CompanyUsers.First();
                Assert.NotNull(addedCompanyAdmin);
                Assert.Equal("Test", addedCompanyAdmin.FirstName);
                Assert.Equal("Admin", addedCompanyAdmin.LastName);
                Assert.Equal("test@company.com", addedCompanyAdmin.Email);
                Assert.Equal(addedCompany.ID, addedCompanyAdmin.CompanyID);

                // Cleanup
                context.Companies.Remove(addedCompany);
                context.SaveChanges();
            }
        }

        [Fact]
        public void AddCompanyWithMultipleUsers()
        {
            var options = CreateNewContextOptions();

            using (var context = new CompanyDbContext(options))
            {
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








    }
}
