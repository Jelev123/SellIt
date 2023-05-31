namespace SellIt.Core.Services.Category
{
    using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment environment;


        public CategoryService(ApplicationDbContext data, IImageService imageService, IWebHostEnvironment environment)
        {
            this.data = data;
            this.imageService = imageService;
            this.environment = environment;
        }

        public Task CreateCategory(CreateCategoryViewModel createCategory)
        {
            var category = new Category
            {
                Name = createCategory.Name,
            };

            var filePath = "images/categories/";
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createCategory.Image.FileName);
            var imagePath = Path.Combine(filePath, fileName);

            // Call the image service to upload the image file
            imageService.UploadImage(imagePath, createCategory.Image);

            category.Image = imagePath;

            data.Categories.Add(category);
            data.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<AllCategoriesViewModel> GetAllCategories<T>()
        {
            var all = this.data.Categories
                .Select(s => new AllCategoriesViewModel
                {
                    Id=s.Id,
                    Name = s.Name,
                    Photo = s.Image
                });

            return all;
        }
    }
}
