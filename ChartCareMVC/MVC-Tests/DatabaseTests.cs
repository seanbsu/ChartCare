using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;
using ChartCareMVC.Areas.Identity.Data;
using ChartCareMVC.Data;
using ChartCareMVC.Models;

namespace MVC_Tests
{
    public class DatabaseTests
    {
        private readonly CompanyDbContext _context;

        public DatabaseTests()
        {
            var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CompanyDbContext(options);
        }

        [Fact]
        public void TestAddCompany()
        {
            // Arrange
            var company = new Company
            {
                ID = 1,
                Name = "Test Company",
                Address = "123 Test St",
                PlanID = 1,
                Email = "test@company.com",
                PricingPlan = new PricingPlan { ID = 1 },
                CompanyUsers = new List<CompanyUser>()
            };

            // Act
            _context.Companies.Add(company);
            _context.SaveChanges();

            // Assert
            var addedCompany = _context.Companies.FirstOrDefault(c => c.ID == 1);
            Assert.NotNull(addedCompany);
            Assert.Equal("Test Company", addedCompany.Name);
            Assert.Equal("123 Test St", addedCompany.Address);
            Assert.Equal("test@company.com", addedCompany.Email);

            // Cleanup
            _context.Companies.Remove(addedCompany);
            _context.SaveChanges();
        }
    }
}
