﻿namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants.Error;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.Handlers.Error;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;

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
            await PopulateCategoriesInViewData();
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct(AddEditProductViewModel addProduct, GalleryFileDTO fileDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                await PopulateCategoriesInViewData();
                return View(addProduct);
            }
            try
            {
                await productService.AddProductAsync(addProduct, fileDTO);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw new Exception("Error add product", ex);
            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return await productService.DeleteProductAsync(id)
                             .ContinueWith(_ => RedirectToAction("Index", "Home"));
        }

        [Authorize]
        public async Task<IActionResult> EditProduct(int id)
        {
            var categories = await GetCategoriesAsViewModelsAsync();

            ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
                CategoryId = s.Id
            }).ToList();

            return await productService.GetByIdAsync(id)
                != null
                ? View(await productService.GetByIdAsync(id))
                : RedirectToAction("HandleError", "Product", new { errorMessage = "Product not found" });

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditProduct(AddEditProductViewModel editProduct, int id, GalleryFileDTO fileDTO)
        {
            var product = productService.GetAllProductsByCategoryIdAsync(id);

            if (product == null)
            {
                throw new DataNotFoundException(string.Format(
                    ErrorMessages.DataDoesNotExist,
                    typeof(Product).Name, "id", id));
            }

            return await productService.EditProductAsync(editProduct, id, fileDTO)
               .ContinueWith(_ => RedirectToAction("MyProducts"));
        }


        public async Task<IActionResult> GetProductById(int id)
        {
            return (await productService.GetByIdAsync(id)) == null
                 ? RedirectToAction("HandleError", "Product", new { errorMessage = "Product not found" })
                 : View(await productService.GetByIdAsync(id));
        }

        public async Task<IActionResult> Search(string searchName)
        {
            return (ViewData["searchProduct"] = searchName) is var _
              && (await searchService.SearchProductAsync(searchName)) is var searchedProduct
              && searchedProduct != null
              ? View(searchedProduct)
              : View(searchName);
        }

        public async Task<IActionResult> SearchCategory(string searchName)
        {
            return (ViewData["searchCategory"] = searchName) is var _ &&
             (await searchService.SearchProductAsync(searchName)) is var searchedCategory && searchedCategory != null
             ? View(searchedCategory)
             : View(searchName);
        }

        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            return View(await productService.FavoritesAsync());
        }

        public async Task<IActionResult> AllProducts()
        {
            return View(await productService.GetAllProductsAsync());
        }

        public async Task<IActionResult> AllProductsByCategoryId(int id)
        {
            return View(await productService.GetAllProductsByCategoryIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Like(int id)
        {
            return View(await productService.LikeAsync(id));
        }


        private async Task<IEnumerable<AddEditProductViewModel>> GetCategoriesAsViewModelsAsync()
        {
            var categories = await categoryService.GetAllCategoriesAsync<AllCategoriesViewModel>();

            return categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
                CategoryId = s.Id
            }).ToList();
        }

        private async Task PopulateCategoriesInViewData()
        {
            var categories = await GetCategoriesAsViewModelsAsync();
            ViewData["categories"] = categories.Select(c => new AddEditProductViewModel
            {
                CategoryName = c.CategoryName,
            }).ToList();
        }
    }
}
