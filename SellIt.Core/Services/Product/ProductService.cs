namespace SellIt.Core.Services.Product
{
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;
        private readonly IImageService imageService;


        public ProductService(ApplicationDbContext data, IImageService imageService)
        {
            this.data = data;
            this.imageService = imageService;
        }

        public Task AddProduct(AddEditProductViewModel addProduct, string userId, string imagePath)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            imageService.CheckGallery(addProduct);
            var category = this.data.Categories.FirstOrDefault(s => s.Name == addProduct.CategoryName);
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Category = category,
                CategoryId = category.Id,
                CreatedUserId = userId,
                Price = addProduct.Price,
                PhoneNumber = addProduct.PhoneNumber,
                ProductAdressId = user.AdressId,
            };


            ProductConstants.IsCreated = true;

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

        public void DeleteProduct(int id)
        {
            var product = this.data.Products.FirstOrDefault(s => s.ProductId == id);
            var productImage = this.data.Images.FirstOrDefault(s => s.ProductId == id);
            data.Remove(productImage);
            data.Remove(product);
            data.SaveChanges();
            ProductConstants.IsDeleted = true;
        }

        public void EditProduct(AddEditProductViewModel editProduct, int id, string userId)
        {
            imageService.CheckGallery(editProduct);
            var product = this.data.Products.FirstOrDefault(s => s.ProductId == id);
            var category = this.data.Categories.FirstOrDefault(s => s.Name == editProduct.CategoryName);
            product.Name = editProduct.Name;
            product.Description = editProduct.Description;
            product.Price = editProduct.Price;
            product.CategoryId = category.Id;

            if (editProduct.GalleryFiles != null)
            {
                foreach (var file in editProduct.Gallery)
                {
                    product.Images.Add(new Image()
                    {
                        Name = file.Name,
                        URL = file.URL,
                        AddedByUserId = userId
                    });
                }
            }
            data.Update(product);
            data.SaveChanges();
        }

        public IEnumerable<AllProductViewModel> GetAllProducts()
        {
            var allProducts = this.data.Products
                .Select(s => new AllProductViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    UserId = s.CreatedUserId,
                    Id = s.ProductId,
                    Price = s.Price,
                    CoverPhoto = s.Images.FirstOrDefault().URL
                });

            return allProducts;
        }

        public GetByIdAndLikeViewModel GetById(int id, string userId)
        {
            var product = this.data.Products
                .Where(s => s.ProductId == id)
                .Select(s => new GetByIdAndLikeViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    CategoryId = s.CategoryId,
                    Description = s.Description,
                    IsAprooved = s.IsAproved,
                    Viewed = s.Viewed,
                    LikedCount = s.LikedCount,
                    UserId = s.CreatedUserId,
                    Price = s.Price,
                    Id = s.ProductId,
                    UserName = s.User.UserName,
                    ProducAddresstId = s.ProductAdressId,
                    AddressId = s.ProductAdressId,
                    Gallery = s.Images.Select(s => new GalleryModel()
                    {
                        Id = s.ImageId,
                        ImageId = s.ImageId,
                        Name = s.Name,
                        URL = s.URL,
                        ProductId = s.ProductId
                    }).ToList(),
                }).FirstOrDefault();

            if (product.UserId != userId)
            {
                var viewdProduct = this.data.Products.FirstOrDefault(s => s.ProductId == id);
                viewdProduct.Viewed++;
                data.SaveChanges();
            }
            return product;
        }


        public GetByIdAndLikeViewModel Like(int id, string currentUserId)
        {
            var currentProduct = data.Products.FirstOrDefault(s => s.ProductId == id);

            var likedProduct = new LikedProduct
            {
                ProductId = currentProduct.ProductId,
                UserId = currentUserId,
            };

            if (!data.LikedProducts.Any(lp => lp.UserId == likedProduct.UserId))
            {
                currentProduct.LikedCount++;
                data.LikedProducts.Add(likedProduct);
            }
            else
            {
                var existingLikedProduct = data.LikedProducts.FirstOrDefault(lp => lp.UserId == likedProduct.UserId);
                if (existingLikedProduct != null)
                {
                    data.LikedProducts.Remove(existingLikedProduct);
                    currentProduct.LikedCount--;
                }
            }

            data.SaveChanges();

            var product = this.data.Products.
            Select(s => new GetByIdAndLikeViewModel
            {
                Name = s.Name,
                CategoryName = s.Category.Name,
                Description = s.Description,
                IsAprooved = s.IsAproved,
                Viewed = s.Viewed,
                LikedCount = s.LikedCount,
                UserId = s.CreatedUserId,
                Id = s.ProductId,
                Gallery = s.Images.Select(s => new GalleryModel()
                {
                    Name = s.Name,
                    URL = s.URL,
                }).ToList(),
            })
            .FirstOrDefault(s => s.Id == id);

            return product;
        }

        public IEnumerable<MyProductsViewModel> MyLikedProducts(string userId)
        {
            var myLikedProduct = this.data.LikedProducts.FirstOrDefault(x => x.UserId == userId);
            if (myLikedProduct != null)
            {
                var myProducts = this.data.Products
                .Where(s => s.ProductId == myLikedProduct.ProductId)
                .Select(x => new MyProductsViewModel
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    UserId = userId,
                    IsAprooved = x.IsAproved,
                    Price = x.Price,
                    CoverPhoto = x.Images.FirstOrDefault().URL
                });

                return myProducts;
            }
            else
            {
                return Enumerable.Empty<MyProductsViewModel>();
            }
        }

        public IEnumerable<MyProductsViewModel> MyProducts(string userId)
        {
            var product = this.data.Products.FirstOrDefault(x => x.CreatedUserId == userId);
            var myProducts = this.data.Products
                .Where(s => s.CreatedUserId == userId)
                .Select(x => new MyProductsViewModel
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    Description = x.Description,
                    UserId = userId,
                    IsAprooved = x.IsAproved,
                    Price = x.Price,
                    CoverPhoto = x.Images.FirstOrDefault().URL
                });

            return myProducts;
        }

        public IEnumerable<IndexRandomViewModel> RandomProducts(int count)
        {
            return this.data.Products.OrderBy(s => Guid.NewGuid())
                .Where(s => s.IsAproved == true)
                .Select(s => new IndexRandomViewModel()
                {
                    Id = s.ProductId,
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    CoverPhoto = s.Images.FirstOrDefault().URL,
                    Price = s.Price,
                    IsAproved = s.IsAproved
                })
                .Take(count);
        }
    }
}
