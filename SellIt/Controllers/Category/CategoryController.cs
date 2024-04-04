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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel create)
        {
            if (!ModelState.IsValid)
            {
                return View(create);
            }

            try
            {
                await categoryService.CreateCategoryAsync(create);
                return Redirect("/");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(create);
            }
        }
    }
}
