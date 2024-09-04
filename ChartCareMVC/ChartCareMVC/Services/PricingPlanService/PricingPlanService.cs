using ChartCareMVC.Data;
using ChartCareMVC.Models;
using Microsoft.EntityFrameworkCore;
using ChartCareMVC.Services;


namespace ChartCareMVC.Services.PricingPlanService
{
    public class PricingPlanService : IPricingPlanService
    {
        private readonly CompanyDbContext _dbContext;

        public PricingPlanService(CompanyDbContext context) 
        {
            _dbContext = context;
        }

        public async Task<Result<List<PricingPlan>>> GetPricingPlansAsync()
        {
            var plans = await _dbContext.PricingPlans.ToListAsync();
            if(!plans.Any())
            {
                return new Result<List<PricingPlan>>
                {
                    Success = false,
                    ErrorMessage = "Pricing Plans not found."
                };
            }

            return new Result<List<PricingPlan>>
            {
                Success = true,
                Data = plans
            };
        }
        
        public async Task<Result<PricingPlan>> GetPricingPlanByIdAsync(int id)
        {
            var plan = await _dbContext.PricingPlans.FindAsync(id);
            if (plan == null)
            {
                return new Result<PricingPlan>
                {
                    Success = false,
                    ErrorMessage = "Pricing plan not found."
                };
            }

            return new Result<PricingPlan>
            {
                Success = true,
                Data = plan
            };
        }

        public async Task<Result<List<Features>>> GetPlanFeaturesAsync(string planName)
        {
            try
            {
                var plan = await _dbContext.PricingPlans
                    .Include(plan => plan.PlanFeatureLinks)
                    .ThenInclude(planFeatures => planFeatures.Feature)
                    .FirstOrDefaultAsync(plan => plan.PlanNameString == planName);
                if (plan == null)
                {
                    return new Result<List<Features>>
                    {
                        Success = false,
                        ErrorMessage = "Plan not found"
                    };
                }
                var features = plan.PlanFeatureLinks.Select(planFeatures => planFeatures.Feature).ToList();
                return new Result<List<Features>> 
                { 
                    Success = true, 
                    Data = features 
                };
            }
            catch (Exception ex) 
            { 
                return new Result<List<Features>> 
                { 
                    Success = false,
                    ErrorMessage = ex.Message 
                };
            }
        }

    }
}
