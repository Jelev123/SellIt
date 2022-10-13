namespace SellIt.Core.Services.Animal
{
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ProductService : IProductService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly ApplicationDbContext data;

        public ProductService(ApplicationDbContext data)
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

        public IEnumerable<AllProductsViewModel> GetAllProducts()
        {
            var allProducts = this.data.Products
                .Select(s => new AllProductsViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    IsAprooved = s.IsAproved,
                    Liked = s.Liked,
                    Viewed = s.Viewed,
                    UserId = s.UserId,
                    Id = s.Id,
                    Image = s.Images.ToString(),
                });

            return allProducts;
        }

        public AllProductsViewModel GetById(int id, string userId)
        {
            var product = this.data.Products
                .Where(s => s.Id == id)
                .Select(s => new AllProductsViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    IsAprooved = s.IsAproved,
                    Viewed = s.Viewed,
                    Liked = s.Liked,
                    UserId = s.UserId,
                    Id = s.Id,
                }).FirstOrDefault();

            if (product.UserId != userId)
            {
                var viewdProduct = this.data.Products.FirstOrDefault(s => s.Id == id);
                viewdProduct.Viewed++;
                data.SaveChanges();
            }

            return product;
        }

        public AllProductsViewModel Like(int id)
        {
            var productToLike = this.data.Products.FirstOrDefault(s => s.Id == id);
         
            var product = this.data.Products.
               Select(s => new AllProductsViewModel
               {
                   Name = s.Name,
                   CategoryName = s.Category.Name,
                   Description = s.Description,
                   IsAprooved = s.IsAproved,
                   Viewed = s.Viewed,
                   Liked = s.Liked,
                   UserId = s.UserId,
                   Id = s.Id,
                   IsLiked = s.IsLiked,
               })
               .FirstOrDefault(s => s.Id == id);

            if (productToLike.UserId == product.UserId && productToLike.IsLiked == true)
            {
                productToLike.Liked--;
                productToLike.IsLiked = false;
                data.SaveChanges();
                return product;
            }

            productToLike.Liked++;
            productToLike.IsLiked = true;
            data.SaveChanges();
            return product;
        }

        public IEnumerable<AllProductsViewModel> MyProducts(string userId)
        {
            var myProducts = this.data.Products
                .Where(s => s.UserId == userId)
                .Select(x => new AllProductsViewModel
                {
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    UserId = userId,
                    IsAprooved = x.IsAproved,
                });

            return myProducts;
        }
    }
}
