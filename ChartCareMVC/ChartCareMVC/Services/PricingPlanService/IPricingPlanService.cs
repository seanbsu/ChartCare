using ChartCareMVC.Models;
using ChartCareMVC.Services;

namespace ChartCareMVC.Services.PricingPlanService
{
    public interface IPricingPlanService
    {
        Task<Result<List<PricingPlan>>> GetPricingPlansAsync();
        Task<Result<PricingPlan>> GetPricingPlanByIdAsync(int id);
        Task<Result<List<Features>>> GetPlanFeatures(string planName);
    }
}
