using ChartCareMVC.Data;
using ChartCareMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ChartCareMVC.Services.FeaturesService
{
    public class FeatureService : IFeatureService
    {
        private readonly CompanyDbContext _dbContext;

        public FeatureService(CompanyDbContext context) {
            _dbContext = context;
        }

        public async Task<Result<List<Features>>> GetAllUniqueFeatures()
        {
            List<Features> features = await _dbContext.Features.ToListAsync();
            if (features.IsNullOrEmpty()) {
                return new Result<List<Features>>
                {
                    Success = false,
                    ErrorMessage = "Features returned empty"
                };
            }
            features.RemoveAll( f => f.Name.ToLower().Contains("employee count"));
            features.Add(new Features { 
                ID = 1,
                Name = "Workforce Account Management",
                Description = "Easily manage your company’s employee accounts with comprehensive tools for creation, deletion, and role assignment. " +
                "This feature allows administrators to efficiently handle the entire lifecycle of workforce accounts, ensuring that each employee has" +
                " the appropriate roles and access levels needed to perform their duties effectively."
            });

            return new Result<List<Features>>
            {
                Success =true,
                Data = features
            };
        }
    }
}
