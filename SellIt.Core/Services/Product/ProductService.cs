namespace SellIt.Core.Services.Product
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public partial class ProductService : IProductService
    {
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly string CurrentUserId;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Image> imageRepository;
        private readonly IRepository<LikedProduct> likedProdudctsRepository;


        public ProductService(IImageService imageService, UserManager<User> userManager, IUserService userService, IRepository<Product> productRepository, IRepository<Image> imageRepository, IRepository<Category> categoryRepository, IRepository<LikedProduct> likedProdudctsRepository)
        {
            this.imageService = imageService;
            this.userManager = userManager;
            this.userService = userService;
            CurrentUserId = userService.CurrentUserAccessor();
            this.productRepository = productRepository;
            this.imageRepository = imageRepository;
            this.categoryRepository = categoryRepository;
            this.likedProdudctsRepository = likedProdudctsRepository;
        }

        public async Task AddProductAsync(AddEditProductViewModel addProduct, GalleryFileDTO fileDTO)
        {
            User currentUser = await userManager.FindByIdAsync(CurrentUserId);
            await this.imageService.CheckGalleryAsync(fileDTO);
            var category = this.categoryRepository.AllAsNoTracking().FirstOrDefault(s => s.Name == addProduct.CategoryName);
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                CategoryId = category.Id,
                CreatedUserId = CurrentUserId,
                Price = addProduct.Price,
                PhoneNumber = addProduct.PhoneNumber != null ? addProduct.PhoneNumber : currentUser.PhoneNumber,
                ProductAdress = addProduct.Address,
                Images = fileDTO.Gallery.Select(file => new Image
                {
                    Name = file.Name,
                    URL = file.URL,
                    AddedByUserId = CurrentUserId
                }).ToList()
            };

            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = this.productRepository.AllAsNoTracking().FirstOrDefault(s => s.ProductId == id);
            if (product != null)
            {
                var productImage = this.imageRepository.All().FirstOrDefault(s => s.ProductId == id);
                if (productImage != null)
                {
                    imageRepository.Delete(productImage);
                }

                productRepository.Delete(product);
                await productRepository.SaveChangesAsync();
            }
        }

        public async Task EditProductAsync(AddEditProductViewModel editProduct, int id, GalleryFileDTO fileDTO)
        {
            await imageService.CheckGalleryAsync(fileDTO);
            var product = this.productRepository.All().FirstOrDefault(s => s.ProductId == id);
            var category = this.categoryRepository.All().FirstOrDefault(s => s.Name == editProduct.CategoryName);

            if (product != null && category != null)
            {
                product.Name = editProduct.Name;
                product.Description = editProduct.Description;
                product.Price = editProduct.Price;
                product.CategoryId = category.Id;

                if (editProduct.GalleryFiles != null)
                {
                    product.Images.Clear();

                    foreach (var file in fileDTO.Gallery)
                    {
                        product.Images.Add(new Image()
                        {
                            Name = file.Name,
                            URL = file.URL,
                            AddedByUserId = CurrentUserId,
                        });
                    }
                }

                productRepository.Update(product);
                await productRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllProductViewModel>> GetAllProductsAsync()
           => await this.productRepository.AllAsNoTracking()
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
            var product = await this.productRepository.AllAsNoTracking()
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

            if (product != null && CurrentUserId != product.UserId)
            {
                var viewdProduct = this.productRepository.All().FirstOrDefault(s => s.ProductId == id);
                viewdProduct.Viewed++;
                await productRepository.SaveChangesAsync();
            }
            return product;
        }


        public async Task<GetByIdAndLikeViewModel> LikeAsync(int id)
        {
            var currentProduct = await productRepository.All().FirstOrDefaultAsync(s => s.ProductId == id);
            var existingLikedProduct = await likedProdudctsRepository.All()
                .FirstOrDefaultAsync(lp => lp.UserId == CurrentUserId
                && lp.ProductId == currentProduct.ProductId);

            if (existingLikedProduct == null)
            {
                var likedProduct = new LikedProduct
                {
                    ProductId = currentProduct.ProductId,
                    UserId = CurrentUserId,
                };

                currentProduct.LikedCount++;
                currentProduct.IsLiked = true;
                await likedProdudctsRepository.AddAsync(likedProduct);
            }
            else
            {
                likedProdudctsRepository.Delete(existingLikedProduct);

                if (currentProduct.LikedCount > 0)
                {
                    currentProduct.LikedCount--;
                    currentProduct.IsLiked = false;

                }
            }

            await likedProdudctsRepository.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<MyProductsViewModel>> FavoritesAsync()
        {
            var myLikedProductIds = await this.likedProdudctsRepository.AllAsNoTracking()
              .Where(x => x.UserId == CurrentUserId)
              .Select(x => x.ProductId)
              .ToListAsync();

            if (myLikedProductIds != null)
            {
                var myProducts = await this.productRepository.All()
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
              => await this.productRepository.AllAsNoTracking()
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
             => await this.productRepository.AllAsNoTracking()
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
