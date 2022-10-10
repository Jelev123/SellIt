namespace SellIt.Core.Services.Category
{
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext data;

        public CategoryService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public Task CreateCategory(CreateCategoryViewModel createCategory)
        {
            var category = new Category
            {
                Name = createCategory.Name,
            };
            data.Categories.Add(category);
            data.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
