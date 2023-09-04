namespace SellIt.Core.Services.Product
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public partial class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly string CurrentUserId;


        public ProductService(ApplicationDbContext data, IImageService imageService, UserManager<User> userManager, IUserService userService)
        {
            this.data = data;
            this.imageService = imageService;
            this.userManager = userManager;
            this.userService = userService;
            CurrentUserId = userService.CurrentUserAccessor();
        }

        public async Task AddProductAsync(AddEditProductViewModel addProduct)
        {
            User currentUser = await userManager.FindByIdAsync(CurrentUserId);
            await this.imageService.CheckGalleryAsync(addProduct);
            var category = this.data.Categories.FirstOrDefault(s => s.Name == addProduct.CategoryName);
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Category = category,
                CategoryId = category.Id,
                CreatedUserId = CurrentUserId,
                Price = addProduct.Price,
                PhoneNumber = addProduct.PhoneNumber != null ? addProduct.PhoneNumber : currentUser.PhoneNumber,
                ProductAdress = addProduct.Address,
                Images = addProduct.Gallery.Select(file => new Image
                {
                    Name = file.Name,
                    URL = file.URL,
                    AddedByUserId = CurrentUserId
                }).ToList()
            };

            await data.AddAsync(product);
            await data.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
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
                await data.SaveChangesAsync();
            }
        }

        public async Task EditProductAsync(AddEditProductViewModel editProduct, int id)
        {
            await imageService.CheckGalleryAsync(editProduct);
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
                    product.Images.Clear();

                    foreach (var file in editProduct.Gallery)
                    {
                        product.Images.Add(new Image()
                        {
                            Name = file.Name,
                            URL = file.URL,
                            AddedByUserId = CurrentUserId,
                        });
                    }
                }

                data.Update(product);
                await data.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllProductViewModel>> GetAllProductsAsync()
           => await this.data.Products
                             .Select(p => new AllProductViewModel
                             {
                                 Name = p.Name,
                                 CategoryName = p.Category.Name,
                                 Description = p.Description,
                                 UserId = p.CreatedUserId,
                                 Id = p.ProductId,
                                 Price = p.Price,
                                 CoverPhoto = p.Images.FirstOrDefault().URL
                             }).ToListAsync();

        public async Task<GetByIdAndLikeViewModel> GetByIdAsync(int id)
        {
            return await this.data.Products
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
                     IsLiked = s.IsLiked,
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
            .FirstOrDefaultAsync();

            //if (product != null && CurrentUserId != product.UserId)
            //{
            //    var viewdProduct = this.data.Products.FirstOrDefault(s => s.ProductId == id);
            //    viewdProduct.Viewed++;
            //    await data.SaveChangesAsync();
            //}
            //return product;
        }


        public async Task<GetByIdAndLikeViewModel> LikeAsync(int id)
        {
            var currentProduct = await data.Products.FirstOrDefaultAsync(s => s.ProductId == id);
            var existingLikedProduct = await data.LikedProducts.FirstOrDefaultAsync(lp => lp.UserId == CurrentUserId && lp.ProductId == currentProduct.ProductId);

            if (existingLikedProduct == null)
            {
                var likedProduct = new LikedProduct
                {
                    ProductId = currentProduct.ProductId,
                    UserId = CurrentUserId,
                };

                currentProduct.LikedCount++;
                currentProduct.IsLiked = true;
                await data.LikedProducts.AddAsync(likedProduct);
            }
            else
            {
                data.LikedProducts.Remove(existingLikedProduct);

                if (currentProduct.LikedCount > 0)
                {
                    currentProduct.LikedCount--;
                    currentProduct.IsLiked = false;

                }
            }

            await data.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<MyProductsViewModel>> FavoritesAsync()
        {
            var myLikedProductIds = await this.data.LikedProducts
              .Where(x => x.UserId == CurrentUserId)
              .Select(x => x.ProductId)
              .ToListAsync();

            if (myLikedProductIds != null)
            {
                var myProducts = await this.data.Products
                     .Where(x => myLikedProductIds.Contains(x.ProductId))
                     .Select(x => new MyProductsViewModel
                     {
                         Id = x.ProductId,
                         Name = x.Name,
                         CategoryName = x.Category.Name,
                         Description = x.Description,
                         UserId = CurrentUserId,
                         Price = x.Price,
                         IsAprooved = x.IsAproved,
                         CoverPhoto = x.Images.FirstOrDefault().URL
                     }).ToListAsync();

                return myProducts;
            }
            else
            {
                return Enumerable.Empty<MyProductsViewModel>();
            }
        }

        public async Task<IEnumerable<IndexRandomViewModel>> RandomProductsAsync(int count)
              => await this.data.Products
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
                                 .Take(count)
                                 .ToListAsync();
        

        public async Task<IEnumerable<AllProductViewModel>> GetAllProductsByCategoryIdAsync(int id)
             => await this.data.Products
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
                          .Where(p => p.CategoryId == id)
                          .ToListAsync();
    }
}
