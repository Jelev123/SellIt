namespace SellIt.Core.Services.Product
{
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.ProductMessage;

    public class ProductService : IProductService
    {
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
                Price = addProduct.Price,
            };

            product.Images = new List<Image>();

            foreach (var file in addProduct.Gallery)
            {
                product.Images.Add(new Image()
                {
                    Name = file.Name,
                    URL = file.URL,
                    AddedByUserId = userId
                });
            }

            data.Add(product);
            data.SaveChanges();
            return Task.CompletedTask;
        }

       

        public IEnumerable<AllProductViewModel> GetAllProducts()
        {
            var allProducts = this.data.Products
                .Select(s => new AllProductViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    UserId = s.UserId,
                    Id = s.Id,
                    Price = s.Price,
                    CoverPhoto = s.Images.FirstOrDefault().URL
                });

            return allProducts;
        }

        public GetByIdAndLikeViewModel GetById(int id, string userId)
        {           
            var product = this.data.Products
                .Where(s => s.Id == id)
                .Select(s => new GetByIdAndLikeViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    IsAprooved = s.IsAproved,
                    Viewed = s.Viewed,
                    LikedCount = s.LikedCount,
                    UserId = s.UserId,
                    Price = s.Price,
                    Id = s.Id,
                    Gallery = s.Images.Select(s => new GalleryModel()
                    {
                        Name = s.Name,
                        URL = s.URL,
                    }).ToList(),
                }).FirstOrDefault();

            if (product.UserId != userId)
            {
                var viewdProduct = this.data.Products.FirstOrDefault(s => s.Id == id);
                viewdProduct.Viewed++;
                data.SaveChanges();
            }

            return product;
        }


        public GetByIdAndLikeViewModel Like(int id, string currentUserId)
        {
            var productToLike = this.data.Products.FirstOrDefault(s => s.Id == id);

            var product = this.data.Products.
              Select(s => new GetByIdAndLikeViewModel
              {
                  Name = s.Name,
                  CategoryName = s.Category.Name,
                  Description = s.Description,
                  IsAprooved = s.IsAproved,
                  Viewed = s.Viewed,
                  LikedCount = s.LikedCount,
                  UserId = s.UserId,
                  Id = s.Id,
                  IsLiked = s.IsLiked,
                  Gallery = s.Images.Select(s => new GalleryModel()
                  {
                      Name = s.Name,
                      URL = s.URL,
                  }).ToList(),
              })
              .FirstOrDefault(s => s.Id == id);

            if (productToLike.UserId != product.UserId && productToLike.IsLiked == true)
            {
                product.LikedCount--;
                product.IsLiked = false;
                productToLike.LikedCount--;
                productToLike.IsLiked = false;
                data.SaveChanges();
                return product;

            }
            else if (productToLike.UserId != product.UserId && productToLike.IsLiked == false)
            {
                product.LikedCount++;
                product.IsLiked = true;
                productToLike.LikedCount++;
                productToLike.IsLiked = true;
                data.SaveChanges();
                return product;

            }

            if (productToLike.UserId == product.UserId && productToLike.IsLiked == true)
            {
                product.LikedCount--;
                product.IsLiked = false;
                productToLike.LikedCount--;
                productToLike.IsLiked = false;
                data.SaveChanges();
                return product;

            }
            else if (productToLike.UserId == product.UserId && productToLike.IsLiked == false)
            {
                product.LikedCount++;
                product.IsLiked = true;
                productToLike.LikedCount++;
                productToLike.IsLiked = true;
                data.SaveChanges();
                return product;

            }

          

            return product;
        }

        public IEnumerable<MyProductsViewModel> MyProducts(string userId)
        {
            var product = this.data.Products.FirstOrDefault(x => x.UserId == userId);
            var myProducts = this.data.Products
                .Where(s => s.UserId == userId)
                .Select(x => new MyProductsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    UserId = userId,
                    IsAprooved = x.IsAproved,
                    Price = x.Price,
                    CoverPhoto = x.Images.FirstOrDefault().URL,
                });

            return myProducts;
        }

        public IEnumerable<IndexRandomViewModel> RandomProducts(int count)
        {
            return this.data.Products.OrderBy(s => Guid.NewGuid())
                .Where(s => s.IsAproved == true)
                .Select(s => new IndexRandomViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    CoverPhoto = s.Images.FirstOrDefault().URL,
                    IsLiked = s.IsLiked,
                    Price = s.Price,
                    IsAproved = s.IsAproved
                })      
                .Take(count);
                
        }
    }
}
