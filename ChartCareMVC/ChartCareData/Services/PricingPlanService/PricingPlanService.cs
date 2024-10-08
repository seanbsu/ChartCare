﻿using ChartCareData.Identity.Data;
using ChartCareData.Models;
using Microsoft.EntityFrameworkCore;
using ChartCareData.Services;


namespace ChartCareData.Services.PricingPlanService
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

        public async Task<Result<Dictionary<PricingPlan, List<Features>>>> GetAllPlansWithFeaturesAsync()
        {
            try
            {
                var plansResult = await GetPricingPlansAsync();
                if (!plansResult.Success || plansResult.Data == null)
                {
                    return new Result<Dictionary<PricingPlan, List<Features>>>
                    {
                        Success = false,
                        ErrorMessage = "Failed to retrieve pricing plans or no plans found."
                    };
                }

                var allPlans = plansResult.Data;
                var result = new Dictionary<PricingPlan, List<Features>>();

                foreach (var plan in allPlans)
                {
                    if (plan == null) continue;

                    var featuresResult = await GetPlanFeaturesAsync(plan.PlanNameString);
                    if (featuresResult.Success && featuresResult.Data != null)
                    {
                        result[plan] = featuresResult.Data;
                    }
                    else
                    {
                        return new Result<Dictionary<PricingPlan, List<Features>>>
                        {
                            Success = false,
                            ErrorMessage = $"Failed to retrieve features for plan: {plan.PlanNameString}"
                        };
                    }
                }

                return new Result<Dictionary<PricingPlan, List<Features>>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new Result<Dictionary<PricingPlan, List<Features>>>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public Result<Dictionary<PricingPlan, List<Features>>> GetCascadedPlansWithFeatures(Dictionary<PricingPlan, List<Features>> plansWithFeatures)
        {
            try
            {
                if (plansWithFeatures == null || !plansWithFeatures.Any())
                {
                    return new Result<Dictionary<PricingPlan, List<Features>>> { Success = false, ErrorMessage = "No plans available." };
                }

                var allFeatures = plansWithFeatures
                    .SelectMany(plan => plan.Value)
                    .Distinct()
                    .ToList();

                var orderedPlans = plansWithFeatures
                    .OrderBy(p => p.Key.ID)
                    .ToList();

                var cascadedPlans = new Dictionary<PricingPlan, List<Features>>();

                foreach (var plan in orderedPlans)
                {
                    var currentPlanFeatures = new List<Features>(plan.Value);
                    cascadedPlans.Add(plan.Key, currentPlanFeatures);
                }

                for (int i = 1; i < orderedPlans.Count; i++)
                {
                    var lowerPlanFeatures = cascadedPlans[orderedPlans[i - 1].Key];
                    var currentPlanFeatures = cascadedPlans[orderedPlans[i].Key];

                    foreach (var feature in lowerPlanFeatures)
                    {
                        if (!feature.Description.Contains("employee accounts") &&
                            !currentPlanFeatures.Any(existingFeature => existingFeature.ID == feature.ID))
                        {
                            currentPlanFeatures.Add(feature);
                        }
                    }
                }

                return new Result<Dictionary<PricingPlan, List<Features>>> { Success = true, Data = cascadedPlans };
            }
            catch (Exception ex)
            {
                return new Result<Dictionary<PricingPlan, List<Features>>> { Success = false, ErrorMessage = $"An error occurred: {ex.Message}" };
            }
        }



    }
}
