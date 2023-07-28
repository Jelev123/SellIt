namespace SellIt.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Home;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ICountService countService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public HomeController(ICountService countService, IProductService productService, ICategoryService categoryService)
        {
            this.countService = countService;
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var count = await this.countService.GetCount(id);
            var counts = new HomeViewModel
            {
                ProductForAprooveCount = count.ProductsToAprooveCount,
                AllProducts = count.AllProducts,
                RandomProducts = await this.productService.RandomProductsAsync(6),
                ProductMessages = count.ProductMessages,
                AllCategories = await this.categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>(),
            };

            ViewData["HomeViewModel"] = counts;

            var categories = await this.categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>();

            this.ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
                CategoryId = s.Id,
                CategoryImage = s.Photo
                
            }).ToList();

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