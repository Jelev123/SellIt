﻿namespace SellIt.Core.Services.Category
{
    using SellIt.Core.Constants;
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

        public async Task CreateCategory(CreateCategoryViewModel createCategory)
        {
            var category = new Category
            {
                Name = createCategory.Name,
                Image = await imageService.UploadImage(CategoryConstants.CategoryImagesFolder, createCategory.Image)
            };

            data.Categories.Add(category);
            data.SaveChanges();
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
