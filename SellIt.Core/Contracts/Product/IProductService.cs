namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        void AddProduct(AddEditProductViewModel addProduct, string userId);

        void DeleteProduct(int id);

        void EditProduct(AddEditProductViewModel editProduct, int id, string userId);

        GetByIdAndLikeViewModel GetById(int id);

        IEnumerable<AllProductViewModel> GetAllProducts();

        IEnumerable<AllProductViewModel> GetAllProductsByCategoryId(int id);

        GetByIdAndLikeViewModel Like(int id, string currentUserId);

        IEnumerable<MyProductsViewModel> Favorites(string id);

        IEnumerable<IndexRandomViewModel> RandomProducts(int count);

    }
}
