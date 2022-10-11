namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
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
            this.productService.AddProduct(addProduct);
            return this.Redirect("/");
        }
    }
}
