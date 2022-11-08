namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ISearchService searchService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext data;

        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<User> userManager, IWebHostEnvironment environment, ApplicationDbContext data, ISearchService searchService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.environment = environment;
            this.data = data;
            this.searchService = searchService;
        }


        [Authorize]
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
            this.productService.AddProduct(addProduct, user, $"{this.environment.WebRootPath}/images");
            return this.Redirect("/");
        }

        public IActionResult GetProductById(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var product = this.productService.GetById(id, userId);
            return this.View(product);
        }
        public IActionResult MyProducts()
        {
            var userId = this.userManager.GetUserId(User);
            var products = this.productService.MyProducts(userId);
            return this.View(products);
        }

        public IActionResult AllProducts()
        {
            var allProducts = this.productService.GetAllProducts();
            return this.View(allProducts);
        }
        public IActionResult Like(int id)
        {
            var currentUserId = this.userManager.GetUserId(User);
            var productToLike = this.productService.Like(id, currentUserId);
            return this.View(productToLike);
        }

        public IActionResult Search(string searchName)
        {
            //if (string.IsNullOrWhiteSpace(searchName))
            //{
            //    return this.View(searchName);
            //}
            this.ViewData["searchProduct"] = searchName;
            var searchedProduct = this.searchService.SearchProduct(searchName);

            if (searchedProduct == null)
            {
                return this.View(searchName);
            }
            return this.View(searchedProduct);
        }

    }
}
