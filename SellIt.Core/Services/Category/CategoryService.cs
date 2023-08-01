namespace SellIt.Core.Services.Category
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext data;
        private readonly IImageService imageService;


        public CategoryService(ApplicationDbContext data, IImageService imageService)
        {
            this.data = data;
            this.imageService = imageService;
        }

        public async Task CreateCategoryAsync(CreateCategoryViewModel createCategory)
        {
            var fileFolder = "images/categories/";
            var category = new Category
            {
                Name = createCategory.Name,
                Image = await imageService.UploadImageAsync(fileFolder, createCategory.Image)
            };

            await data.Categories.AddAsync(category);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllCategoriesViewModel>> GetAllCategoriesAsync<T>()
           => await this.data.Categories
                .Select(s => new AllCategoriesViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Photo = s.Image
                }).ToListAsync();
    }
}
