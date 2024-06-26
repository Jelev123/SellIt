﻿namespace SellIt.Core.Services.Product
{
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class ProductDecoratorService : IProductService
    {
        private readonly ProductService _productService;
        private readonly IImageService imageService;
        private readonly Dictionary<int, GetByIdAndLikeViewModel> getByIdCache = new Dictionary<int, GetByIdAndLikeViewModel>();


        public ProductDecoratorService(ProductService productService, IImageService imageService)
        {
            this._productService = productService;
            this.imageService = imageService;
        }

        public async Task AddProductAsync(AddEditProductViewModel addProduct, GalleryFileDTO fileDTO)
        {
            await this.imageService.CheckGalleryAsync(fileDTO);
            await _productService.AddProductAsync(addProduct, fileDTO);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productService.DeleteProductAsync(id);
        }

        public async Task EditProductAsync(AddEditProductViewModel editProduct, int id, GalleryFileDTO fileDTO)
        {
            await this.imageService.CheckGalleryAsync(fileDTO);
            await _productService.EditProductAsync(editProduct, id, fileDTO);
        }

        public async Task<IEnumerable<MyProductsViewModel>> FavoritesAsync()
        {
            return await _productService.FavoritesAsync();
        }

        public async Task<IEnumerable<AllProductViewModel>> GetAllProductsAsync()
        {
            return await _productService.GetAllProductsAsync();
        }

        public async Task<IEnumerable<AllProductViewModel>> GetAllProductsByCategoryIdAsync(int id)
        {
            return await _productService.GetAllProductsByCategoryIdAsync(id);
        }

        public async Task<GetByIdAndLikeViewModel> GetByIdAsync(int id)
        {
            if (getByIdCache.ContainsKey(id))
            {
                return getByIdCache[id];
            }

            var product = await _productService.GetByIdAsync(id);

            getByIdCache[id] = product;

            return product;
        }

        public async Task<GetByIdAndLikeViewModel> LikeAsync(int id)
        {
            return await _productService.LikeAsync(id);
        }

        public async Task<IEnumerable<IndexRandomViewModel>> RandomProductsAsync(int count)
        {
            return await this._productService.RandomProductsAsync(count);
        }

    }
}
