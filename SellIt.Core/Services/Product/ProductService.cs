namespace SellIt.Core.Services.Animal
{
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using SellIt.Core.ViewModels.Image;

    public class ProductService : IProductService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly ApplicationDbContext data;

        public ProductService(ApplicationDbContext data, UserManager<User> userManager)
        {
            this.data = data;
        }

        public Task AddProduct(AddProductViewModel addProduct, string userId, string imagePath)
        {

            var category = this.data.Categories.FirstOrDefault(s => s.Name == addProduct.CategoryName);
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Category = category,
                UserId = userId,
            };


            Directory.CreateDirectory($"{imagePath}/products/");
            foreach (var image in addProduct.Image)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                product.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                image.CopyToAsync(fileStream);
            }

            data.Add(product);
            data.SaveChanges();
            return Task.CompletedTask;
        }

        public AllProductsViewModel GetById(int id)
        {
            var product = this.data.Products
                .Where(s => s.Id == id)
                .Select(s => new AllProductsViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    Image = s.Images.Select(g => new GalleryModel
                    {
                        Id = g.Id,
                        Extension = g.Extension,
                        URL = g.RemoteImageUrl
                    }).ToList(),
                }).FirstOrDefault();

            return product;
        }
    }
}
