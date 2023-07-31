namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ISearchService searchService;
        public ProductController(IProductService productService, ICategoryService categoryService, ISearchService searchService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.searchService = searchService;
        }


        [Authorize]
        public async Task<IActionResult> AddProduct()
        {
            var categories = await this.categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>();
            this.ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddEditProductViewModel addProduct)
        {
            await this.productService.AddProductAsync(addProduct);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await this.productService.DeleteProductAsync(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var categories = await this.categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>();

            ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
                CategoryId = s.Id
            }).ToList();

            var product = await this.productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(AddEditProductViewModel editProduct, int id)
        {
            await this.productService.EditProductAsync(editProduct, id);
            return this.RedirectToAction("MyProducts");
        }

        public async Task<IActionResult> GetProductById(int id)
        {
            var productById = await this.productService.GetByIdAsync(id);
            if (productById == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(productById);
        }

        public async Task<IActionResult> Search(string searchName)
        {
            this.ViewData["searchProduct"] = searchName;
            var searchedProduct = await this.searchService.SearchProductAsync(searchName);

            if (searchedProduct == null)
            {
                return this.View(searchName);
            }

            return this.View(searchedProduct);
        }

        public async Task<IActionResult> SearchCategory(string searchName)
        {
            this.ViewData["searchCategory"] = searchName;
            var searchedCategory = await this.searchService.SearchProductAsync(searchName);

            if (searchedCategory == null)
            {
                return this.View(searchName);
            }

            return this.View(searchedCategory);
        }

        public async Task<IActionResult> Favorites() => View(await this.productService.FavoritesAsync());

        public async Task<IActionResult> AllProducts() => View(await this.productService.GetAllProductsAsync());

        public async Task<IActionResult> AllProductsByCategoryId(int id) => View(await this.productService.GetAllProductsByCategoryIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Like(int id) => View(await this.productService.LikeAsync(id));
    }
}
