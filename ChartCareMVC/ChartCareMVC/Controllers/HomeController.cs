using ChartCareMVC.Models;
using ChartCareMVC.Services;
using ChartCareMVC.Services.PricingPlanService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Diagnostics;

namespace ChartCareMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPricingPlanService _pricingPlanService;

        public HomeController(ILogger<HomeController> logger, IPricingPlanService pricingPlanService)
        {
            _logger = logger;
            _pricingPlanService = pricingPlanService;
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

            //if (allPlansResult.Success)
            //{
            //    if (allPlansResult.Data != null)
            //    {
            //        var cascadedPlansResult = _pricingPlanService.GetCascadedPlansWithFeatures(allPlansResult.Data);
            //        if (cascadedPlansResult.Success)
            //        {
            //            ViewData["CascadedPlans"] = cascadedPlansResult.Data;
            //        }
            //        else
            //        {
            //            _logger.LogError("Failed to retrieve ordered pricing plans: {ErrorMessage}", cascadedPlansResult.ErrorMessage);
            //        }
            //    }
            //    else
            //    {
            //        _logger.LogError("Retrieved pricing plans data is null.");
            //    }

            //    ViewData["Plans"] = allPlansResult.Data;
            //}
            //else
            //{
            //    _logger.LogError("Failed to retrieve pricing plans: {ErrorMessage}", allPlansResult.ErrorMessage);
            //}

            if (!allPlansResult.Success)
            {
                _logger.LogError("Failed to retrieve pricing plans: {ErrorMessage}", allPlansResult.ErrorMessage);
                Error();
            }
            if(allPlansResult.Data == null )
            {
                _logger.LogError("Retrieved pricing plans data is null.");
                Error();
            }
            var cascadedPlansResult = _pricingPlanService.GetCascadedPlansWithFeatures(allPlansResult.Data);
            if (!cascadedPlansResult.Success)
            {
                _logger.LogError("Failed to retrieve ordered pricing plans: {ErrorMessage}", cascadedPlansResult.ErrorMessage);
                Error();
            }
            ViewData["Plans"] = allPlansResult.Data;
            ViewData["CascadedPlans"] = cascadedPlansResult.Data;
            return View();
        }

    }
}
