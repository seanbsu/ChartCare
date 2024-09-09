using Microsoft.AspNetCore.Mvc.RazorPages;
using ChartCareMVC.Services.PricingPlanService;
using System.Dynamic;

namespace ChartCareMVC.Views.Home
{
    public class FeaturesModel : PageModel
    {
        private readonly IPricingPlanService _pricingPlanService;

        public FeaturesModel(IPricingPlanService pricingPlanService)
        {
            _pricingPlanService = pricingPlanService;
        }

        public async Task OnGetAsync()
        {

        }
    }
}
