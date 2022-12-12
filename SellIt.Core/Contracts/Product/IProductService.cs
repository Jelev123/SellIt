namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        Task AddProduct(AddProductViewModel addProduct, string userId, string imagePath);

        void DeleteProduct(int id);

        GetByIdAndLikeViewModel GetById(int id, string userId);

        IEnumerable<AllProductViewModel> GetAllProducts();

        GetByIdAndLikeViewModel Like(int id, string currentUserId);

        IEnumerable<MyProductsViewModel> MyProducts(string id);

        IEnumerable<IndexRandomViewModel> RandomProducts(int count);

    }
}
