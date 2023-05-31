namespace SellIt.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Home;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using SellIt.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ICountService countService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<User> userManager;

        public HomeController(ICountService countService, IProductService productService, UserManager<User> userManager, ICategoryService categoryService)
        {
            this.countService = countService;
            this.productService = productService;
            this.userManager = userManager;
            this.categoryService = categoryService;
        }

        public IActionResult Index(int id)
        {
            var count = this.countService.GetCount(id);
            var counts = new HomeViewModel
            {
                ProductForAprooveCount = count.ProductsToAprooveCount,
                AllProducts = count.AllProducts,
                RandomProducts = this.productService.RandomProducts(6).ToList(),
                ProductMessages = count.ProductMessages,
                AllCategories = this.categoryService.GetAllCategories<AllCategoriesViewModel>(),
            };

            ViewData["HomeViewModel"] = counts;

            var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

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