namespace SellIt.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Home;
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
            var count = await this.countService.GetCountAsync(id);
            var counts = new HomeViewModel
            {
                ProductForAprooveCount = count.ProductsToAprooveCount,
                AllProducts = count.AllProducts,
                RandomProducts = await productService.RandomProductsAsync(6),
                ProductMessages = count.ProductMessages,
                AllCategories = await categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>(),
            };

            ViewData["HomeViewModel"] = counts;

            var categories = await categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>();

            this.ViewData["categories"] = categories.Select(s => new AllCategoriesViewModel
            {
                Name = s.Name,
                Id = s.Id,
                Photo = s.Photo
                
            }).ToList();

            return View(counts);
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