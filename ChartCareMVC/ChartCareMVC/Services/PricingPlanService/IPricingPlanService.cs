using ChartCareMVC.Models;
using ChartCareMVC.Services;

namespace ChartCareMVC.Services.PricingPlanService
{
    public interface IPricingPlanService
    {
        /// <summary>
        /// Asynchronously retrieves a list of all pricing plans.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> with a list of <see cref="PricingPlan"/> objects if successful, otherwise an error message.</returns>
        Task<Result<List<PricingPlan>>> GetPricingPlansAsync();

        /// <summary>
        /// Asynchronously retrieves a specific pricing plan by its ID.
        /// </summary>
        /// <param name="id">The ID of the pricing plan to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> with the <see cref="PricingPlan"/> object if found, otherwise an error message.</returns>
        Task<Result<PricingPlan>> GetPricingPlanByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a list of features associated with a specific pricing plan by its name.
        /// </summary>
        /// <param name="planName">The name of the pricing plan whose features are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> with a list of <see cref="Features"/> objects if successful, otherwise an error message.</returns>
        Task<Result<List<Features>>> GetPlanFeaturesAsync(string planName);

        /// <summary>
        /// Asynchronously retrieves all pricing plans and their associated features.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> with a dictionary where each <see cref="PricingPlan"/> is associated with a list of <see cref="Features"/> objects if successful, otherwise an error message.</returns>
        Task<Result<Dictionary<PricingPlan, List<Features>>>> GetAllPlansWithFeaturesAsync();

        /// <summary>
        /// Processes a dictionary of pricing plans and their features to produce a cascaded view where higher-tier plans include features of lower-tier plans.
        /// </summary>
        /// <param name="plansWithFeatures">A dictionary where each <see cref="PricingPlan"/> is associated with a list of <see cref="Features"/>.</param>
        /// <returns>A <see cref="Result{T}"/> containing a dictionary where each <see cref="PricingPlan"/> is associated with a list of <see cref="Features"/> in a cascaded manner if successful, otherwise an error message.</returns>
        Result<Dictionary<PricingPlan, List<Features>>> GetCascadedPlansWithFeatures(Dictionary<PricingPlan, List<Features>> plansWithFeatures);
    }
}
