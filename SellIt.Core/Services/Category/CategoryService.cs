namespace SellIt.Core.Services.Category
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Windows.Storage;

    public class CategoryService : ICategoryService
    {
        private readonly IImageService imageService;
        private readonly IRepository<Category> categoryRepository;


        public CategoryService(IImageService imageService, IRepository<Category> categoryRepository)
        {
            this.imageService = imageService;
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(CreateCategoryViewModel createCategory)
        {
            var fileFolder = CategoryConstants.CategoryImagesFolder;
            var category = new Category
            {
                Name = createCategory.Name,
                Image = await imageService.UploadImageAsync(fileFolder, createCategory.Image)
            };

            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllCategoriesViewModel>> GetAllCategoriesAsync<T>()
        {
            return await this.categoryRepository.AllAsNoTracking()
                  .Select(s => new AllCategoriesViewModel
                  {
                      Id = s.Id,
                      Name = s.Name,
                      Photo = s.Image
                  }).ToListAsync();
        }
    }
}
