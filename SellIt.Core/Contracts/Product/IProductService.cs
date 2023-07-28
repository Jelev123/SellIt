namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        Task AddProductAsync(AddEditProductViewModel addProduct);

        Task DeleteProductAsync(int id);

        Task EditProductAsync(AddEditProductViewModel editProduct, int id);

        Task<GetByIdAndLikeViewModel> GetByIdAsync(int id);

        Task<IEnumerable<AllProductViewModel>> GetAllProductsAsync();

        Task<IEnumerable<AllProductViewModel>> GetAllProductsByCategoryIdAsync(int id);

        Task<GetByIdAndLikeViewModel> LikeAsync(int id);

        Task<IEnumerable<MyProductsViewModel>> FavoritesAsync();

        Task<IEnumerable<IndexRandomViewModel>> RandomProductsAsync(int count);

    }
}
