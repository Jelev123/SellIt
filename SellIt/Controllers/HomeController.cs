﻿namespace SellIt.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Home;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using SellIt.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ICountService countService;
        private readonly IProductService productService;
        private readonly UserManager<User> userManager;

        public HomeController(ICountService countService, IProductService productService, UserManager<User> userManager)
        {
            this.countService = countService;
            this.productService = productService;
            this.userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var count = this.countService.GetCount(id);
            var counts = new HomeViewModel
            {
                ProductForAprooveCount = count.ProductsToAprooveCount,
                AllProducts = count.AllProducts,
                RandomProducts = this.productService.RandomProducts(6),
                ProductMessages = count.ProductMessages,
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