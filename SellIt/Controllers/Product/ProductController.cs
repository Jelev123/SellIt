namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment environment;

        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<User> userManager, IWebHostEnvironment environment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult AddProduct()
        {
            var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

            this.ViewData["categories"] = categories.Select(s => new AddProductViewModel
            {
                CategoryName = s.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductViewModel addProduct)
        {
            var user = this.userManager.GetUserId(User);

            this.productService.AddProduct(addProduct,user, $"{this.environment.WebRootPath}/images");
            return this.Redirect("/");
        }

        public IActionResult GetProductById(int id)
        {
            var product = this.productService.GetById(id);
            return this.View(product);
        }
        public IActionResult MyProducts()
        {
            var userId = this.userManager.GetUserId(User);

            var products = this.productService.MyProducts(userId);
            return this.View(products);
        }
    }
}
