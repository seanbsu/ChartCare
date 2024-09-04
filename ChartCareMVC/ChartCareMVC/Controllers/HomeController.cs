using ChartCareMVC.Models;
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
            var result = await _pricingPlanService.GetAllPlansWithFeaturesAsync();
            if (result.Success)
            {
                ViewData["Plans"] = result.Data;
            }
            else
            {
                _logger.LogError("Failed to retrieve pricing plans: {ErrorMessage}", result.ErrorMessage);
            }
            return View();
        }
    }
}
