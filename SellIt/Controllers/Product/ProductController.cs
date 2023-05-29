namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Adress;
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
        private readonly IAdressService adressService;
        private readonly ApplicationDbContext data;
        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<User> userManager, IWebHostEnvironment environment, ISearchService searchService, IAdressService adressService, ApplicationDbContext data)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.environment = environment;
            this.searchService = searchService;
            this.adressService = adressService;
            this.data = data;
        }


        [Authorize]
        public IActionResult AddProduct()
        {
            var userId = this.userManager.GetUserId(User);

            var address = this.adressService.AddressByUserId(userId);

            this.ViewData["address"] = address.City;

            var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

            this.ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddEditProductViewModel addProduct)
        {
            var user = this.userManager.GetUserId(User);
            this.productService.AddProduct(addProduct, user, $"{this.environment.WebRootPath}/images");
            return this.Redirect("/");
        }

        public IActionResult DeleteProduct(int id)
        {
            this.productService.DeleteProduct(id);
            return this.Redirect("/");
        }

        public IActionResult EditProduct(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

            this.ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
                CategoryId = s.Id,

            }).ToList();

            var product = this.productService.GetById(id, userId);
            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(AddEditProductViewModel editProduct, int id)
        {
            var userId = this.userManager.GetUserId(User);
            this.productService.EditProduct(editProduct, id, userId);
            return this.RedirectToAction("MyProducts");
        }

        public IActionResult GetProductById(int id)
        {
            var product = this.data.Products.FirstOrDefault(s => s.ProductId == id);
            var userId = product.CreatedUserId;
            var address = this.adressService.AddressByUserId(userId);

            this.ViewData["address"] = address.City;

            var productById = this.productService.GetById(id, userId);
            return this.View(productById);
        }
        public IActionResult MyProducts()
        {
            var userId = this.userManager.GetUserId(User);
            var products = this.productService.MyProducts(userId);
            return this.View(products);
        }

        public IActionResult Favorites()
        {
            var userId = this.userManager.GetUserId(User);
            var myLikedProducts = this.productService.Favorites(userId);
            return this.View(myLikedProducts);
        }

        public IActionResult AllProducts()
        {
            var allProducts = this.productService.GetAllProducts();
            return this.View(allProducts);
        }

        [HttpPost]
        public IActionResult Like(int id)
        {
            var currentUserId = this.userManager.GetUserId(User);
            var productToLike = this.productService.Like(id, currentUserId);
            return this.View(productToLike);
        }

        public IActionResult Search(string searchName)
        {
            this.ViewData["searchProduct"] = searchName;
            var searchedProduct = this.searchService.SearchProduct(searchName);

            if (searchedProduct == null)
            {
                return this.View(searchName);
            }
            return this.View(searchedProduct);
        }

        public IActionResult SearchCategory(string searchName)
        {
            this.ViewData["searchCategory"] = searchName;
            var searchedCategory = this.searchService.SearchProduct(searchName);

            if (searchedCategory == null)
            {
                return this.View(searchName);
            }
            return this.View(searchedCategory);
        }
    }
}
