namespace SellIt.Core.Services.Animal
{
    using SellIt.Core.Contracts.Cloudinary;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;
        private readonly ICloduinaryService cloduinaryService;
        private readonly UserManager<User> userManager;

        public ProductService(ApplicationDbContext data, ICloduinaryService cloduinaryService, UserManager<User> userManager)
        {
            this.data = data;
            this.cloduinaryService = cloduinaryService;
            this.userManager = userManager;
        }

        public async Task AddProduct(AddProductViewModel addProduct)
        {

            var userId = this.userManager.Users.First().Id;
            string imageUrl = await this.cloduinaryService.UploadPictureAsync(
                addProduct.Image,
                addProduct.Name);

            var category = data.Categories.FirstOrDefault(s => s.Name == addProduct.CategoryName);
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Category = category,
                Image = imageUrl,
                UserId = userId,

            };

            data.Add(product);
            data.SaveChanges();

        }
    }
}
