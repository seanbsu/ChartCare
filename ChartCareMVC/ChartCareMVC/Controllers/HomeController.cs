using ChartCareData.Models;
using ChartCareData.Services;
using ChartCareData.Services.FeaturesService;
using ChartCareData.Services.PricingPlanService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace ChartCareMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPricingPlanService _pricingPlanService;
        private readonly IFeatureService _featureService;

        public HomeController(ILogger<HomeController> logger, IPricingPlanService pricingPlanService, IFeatureService featureService)
        {
            _logger = logger;
            _pricingPlanService = pricingPlanService;
            _featureService =featureService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Pricing()
        {
            ViewData["Title"] = "Pricing";

            var allPlansResult = await _pricingPlanService.GetAllPlansWithFeaturesAsync();
            if (!allPlansResult.Success)
            {
                _logger.LogError("Failed to retrieve pricing plans: {ErrorMessage}", allPlansResult.ErrorMessage);
                return Error();
            }

            if(allPlansResult.Data == null )
            {
                _logger.LogError("Retrieved pricing plans data is null.");
                return Error();
            }

            var cascadedPlansResult = _pricingPlanService.GetCascadedPlansWithFeatures(allPlansResult.Data);
            if (!cascadedPlansResult.Success)
            {
                _logger.LogError("Failed to retrieve ordered pricing plans: {ErrorMessage}", cascadedPlansResult.ErrorMessage);
                return Error();
            }

            ViewData["Plans"] = allPlansResult.Data;
            ViewData["CascadedPlans"] = cascadedPlansResult.Data;
            return View();
        }

        public async Task<IActionResult> Features()
        {
            var result = await _featureService.GetAllUniqueFeatures();
            if (!result.Success || result.Data == null || !result.Data.Any())
            {
                _logger.LogError("Failed to retrieve features: {ErrorMessage}", result.ErrorMessage);
                return Error();
            }

            var viewModel = result.Data;
            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }


    }
}
