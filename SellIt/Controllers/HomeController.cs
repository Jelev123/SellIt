namespace SellIt.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.ViewModels.Home;
    using SellIt.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICountService countService;


        public HomeController(ILogger<HomeController> logger, ICountService countService)
        {
            _logger = logger;
            this.countService = countService;
        }

        public IActionResult Index()
        {

            var count = this.countService.GetCount();
            var counts = new HomeViewModel
            {
                ProductForAprooveCount = count.ProductsToAprooveCount,
                AllProducts = count.AllProducts,
            };

            ViewData["HomeViewModel"] = counts;

            ViewData[MessageConstants.SuccessMessage] = "Welcome!";

            return this.View(counts);


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
    }
}