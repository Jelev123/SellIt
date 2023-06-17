namespace SellIt.Core.Services.Product
{
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public partial class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;
        private readonly IImageService imageService;


        public ProductService(ApplicationDbContext data, IImageService imageService)
        {
            this.data = data;
            this.imageService = imageService;
        }

        public void AddProduct(AddEditProductViewModel addProduct, string userId)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            this.imageService.CheckGallery(addProduct);
            var category = this.data.Categories.FirstOrDefault(s => s.Name == addProduct.CategoryName);
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Category = category,
                CategoryId = category.Id,
                CreatedUserId = userId,
                Price = addProduct.Price,
                PhoneNumber = addProduct.PhoneNumber != null ? addProduct.PhoneNumber : user.PhoneNumber,
                ProductAdress = addProduct.Address,
                Images = addProduct.Gallery.Select(file => new Image
                {
                    Name = file.Name,
                    URL = file.URL,
                    AddedByUserId = userId
                }).ToList()
            };

            data.Add(product);
            data.SaveChanges();     
        }

        public void DeleteProduct(int id)
        {
            var product = this.data.Products.FirstOrDefault(s => s.ProductId == id);
            if (product != null)
            {
                var productImage = this.data.Images.FirstOrDefault(s => s.ProductId == id);
                if (productImage != null)
                {
                    data.Remove(productImage);
                }

                data.Remove(product);
                data.SaveChanges();
            }
        }

        public void EditProduct(AddEditProductViewModel editProduct, int id, string userId)
        {
            imageService.CheckGallery(editProduct);
            var product = this.data.Products.FirstOrDefault(s => s.ProductId == id);
            var category = this.data.Categories.FirstOrDefault(s => s.Name == editProduct.CategoryName);

            if (product != null && category != null)
            {
                product.Name = editProduct.Name;
                product.Description = editProduct.Description;
                product.Price = editProduct.Price;
                product.CategoryId = category.Id;

                if (editProduct.GalleryFiles != null)
                {
                    product.Images.Clear(); // Clear existing images before adding new ones

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
        }

        public IEnumerable<AllProductViewModel> GetAllProducts()
        {
            var allProducts = this.data.Products
                .Select(p => new AllProductViewModel
                {
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    UserId = p.CreatedUserId,
                    Id = p.ProductId,
                    Price = p.Price,
                    CoverPhoto = p.Images.FirstOrDefault().URL
                });

            return allProducts;
        }

        public GetByIdAndLikeViewModel GetById(int id)
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
                     ProducAddress = s.ProductAdress,
                     Gallery = s.Images.Select(img => new GalleryModel
                     {
                         Id = img.ImageId,
                         ImageId = img.ImageId,
                         Name = img.Name,
                         URL = img.URL,
                         ProductId = img.ProductId
                     }).ToList(),
                 })
                 .FirstOrDefault();

            return product;
        }


        public GetByIdAndLikeViewModel Like(int id, string currentUserId)
        {
            var currentProduct = data.Products.FirstOrDefault(s => s.ProductId == id);

            var existingLikedProduct = data.LikedProducts.FirstOrDefault(lp => lp.UserId == currentUserId && lp.ProductId == currentProduct.ProductId);

            if (existingLikedProduct == null)
            {
                var likedProduct = new LikedProduct
                {
                    ProductId = currentProduct.ProductId,
                    UserId = currentUserId,
                };

                currentProduct.LikedCount++;
                data.LikedProducts.Add(likedProduct);
            }
            else
            {
                data.LikedProducts.Remove(existingLikedProduct);

                if (currentProduct.LikedCount > 0)
                {
                    currentProduct.LikedCount--;
                }
            }

            data.SaveChanges();

            var product = GetById(id);

            return product;
        }

        public IEnumerable<MyProductsViewModel> Favorites(string userId)
        {
            var myLikedProductIds = this.data.LikedProducts
              .Where(x => x.UserId == userId)
              .Select(x => x.ProductId)
              .ToList();

            if (myLikedProductIds != null)
            {
                var myProducts = this.data.Products
                     .Where(x => myLikedProductIds.Contains(x.ProductId))
                     .Select(x => new MyProductsViewModel
                     {
                         Id = x.ProductId,
                         Name = x.Name,
                         CategoryName = x.Category.Name,
                         Description = x.Description,
                         UserId = userId,
                         Price = x.Price,
                         IsAprooved = x.IsAproved,
                         CoverPhoto = x.Images.FirstOrDefault().URL
                     }).ToList();

                return myProducts;
            }
            else
            {
                return Enumerable.Empty<MyProductsViewModel>();
            }
        }

        public IEnumerable<IndexRandomViewModel> RandomProducts(int count)
        {
            return this.data.Products
                  .Where(s => s.IsAproved == true)
                  .OrderBy(s => Guid.NewGuid())
                  .Select(s => new IndexRandomViewModel()
                  {
                      Id = s.ProductId,
                      Name = s.Name,
                      CategoryName = s.Category.Name,
                      CoverPhoto = s.Images.FirstOrDefault().URL,
                      Price = s.Price,
                      IsAproved = s.IsAproved,
                      LikedCount = s.LikedCount,
                      CategoryImage = s.Category.Image,
                      Description = s.Description,
                  })
                   .Take(count); 
        }

        public IEnumerable<AllProductViewModel> GetAllProductsByCategoryId(int id)
        {
           return this.data.Products
                .Select(p => new AllProductViewModel
                {
                    Name = p.Name,
                    Id = p.ProductId,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    CategoryId = p.CategoryId,
                    Price = p.Price,
                    CoverPhoto = p.Images.FirstOrDefault().URL,
                })
                .Where(p => p.CategoryId == id);
        }
    }
}
