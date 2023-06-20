﻿namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.Search;
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
        public IActionResult AddProduct()
        {
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
            this.productService.AddProduct(addProduct);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteProduct(int id)
        {
            try
            {
                this.productService.DeleteProduct(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public IActionResult EditProduct(int id)
        {
            var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

            ViewData["categories"] = categories.Select(s => new AddEditProductViewModel
            {
                CategoryName = s.Name,
                CategoryId = s.Id
            }).ToList();

            var product = this.productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(AddEditProductViewModel editProduct, int id)
        {
            this.productService.EditProduct(editProduct, id);
            return this.RedirectToAction("MyProducts");
        }

        public IActionResult GetProductById(int id)
        {
            var productById = this.productService.GetById(id);
            if (productById == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(productById);
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

        public IActionResult Favorites() => View(this.productService.Favorites());

        public IActionResult AllProducts() => View(this.productService.GetAllProducts());

        public IActionResult AllProductsByCategoryId(int id) => View(this.productService.GetAllProductsByCategoryId(id));

        [HttpPost]
        public IActionResult Like(int id) => View(this.productService.Like(id));
    }
}
