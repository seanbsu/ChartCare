using ChartCareData.Identity.Data;
using ChartCareData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ChartCareData.Services.FeaturesService
{
    public class FeatureService : IFeatureService
    {
        private readonly CompanyDbContext _dbContext;

        public FeatureService(CompanyDbContext context) {
            _dbContext = context;
        }

        public async Task<Result<List<FeaturesViewModel>>> GetAllUniqueFeatures()
        {
            List<Features> features = await _dbContext.Features.ToListAsync();
            if (features.IsNullOrEmpty())
            {
                return new Result<List<FeaturesViewModel>>
                {
                    Success = false,
                    ErrorMessage = "Features returned empty"
                };
            }

            features.RemoveAll(f => f.Name.ToLower().Contains("employee count"));
            features.Add(new Features
            {
                ID = 1,
                Name = "Workforce Account Management",
                Description = "Easily manage your company’s employee accounts with comprehensive tools for creation, deletion, and role assignment. " +
                "This feature allows administrators to efficiently handle the entire lifecycle of workforce accounts, ensuring that each employee has" +
                " the appropriate roles and access levels needed to perform their duties effectively."
            });

            // Convert Features to FeaturesViewModel and compute image paths
            var viewModels = features.Select(f => new FeaturesViewModel
            {
                Name = f.Name,
                Description = f.Description,
                ImagePath = f.Name == "Workforce Account Management"
                    ? "/images/features-icons/company-users-feature-icon.png"
                    : $"/images/features-icons/{f.Name.ToLower().Replace(" ", "-")}-icon.png"
            }).ToList();

            return new Result<List<FeaturesViewModel>>
            {
                Success = true,
                Data = viewModels
            };
        }

    }
}
