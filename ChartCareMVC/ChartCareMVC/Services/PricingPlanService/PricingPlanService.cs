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

        public async Task<Result<Dictionary<string, List<Features>>>> GetAllPlansWithFeaturesAsync()
        {
            try
            {
                // Retrieve all pricing plans
                var plansResult = await GetPricingPlansAsync();
                if (!plansResult.Success || plansResult.Data == null)
                {
                    return new Result<Dictionary<string, List<Features>>>
                    {
                        Success = false,
                        ErrorMessage = "Failed to retrieve pricing plans or no plans found."
                    };
                }

                var allPlans = plansResult.Data;
                var result = new Dictionary<string, List<Features>>();

                foreach (var plan in allPlans)
                {
                    if (plan == null) continue;

                    var featuresResult = await GetPlanFeaturesAsync(plan.PlanNameString);
                    if (featuresResult.Success && featuresResult.Data != null)
                    {
                        result[plan.PlanNameString] = featuresResult.Data;
                    }
                    else
                    {
                        return new Result<Dictionary<string, List<Features>>>
                        {
                            Success = false,
                            ErrorMessage = $"Failed to retrieve features for plan: {plan.PlanNameString}"
                        };
                    }
                }

                return new Result<Dictionary<string, List<Features>>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new Result<Dictionary<string, List<Features>>>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }


    }
}
