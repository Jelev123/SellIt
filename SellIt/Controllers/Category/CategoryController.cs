namespace SellIt.Controllers.Category
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.ViewModels.Category;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment environment;


        public CategoryController(ICategoryService categoryService, IWebHostEnvironment environment)
        {
            this.categoryService = categoryService;
            this.environment = environment;
        }

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel create)
        {
            this.categoryService.CreateCategory(create, $"{this.environment.WebRootPath}/images");
            return this.Redirect("/");
        }
    }
}
