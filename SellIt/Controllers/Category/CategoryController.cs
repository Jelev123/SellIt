namespace SellIt.Controllers.Category
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.ViewModels.Category;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel create)
        {
            await this.categoryService.CreateCategoryAsync(create);
            return this.Redirect("/");
        }
    }
}
