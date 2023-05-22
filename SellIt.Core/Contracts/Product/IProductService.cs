namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        Task AddProduct(AddEditProductViewModel addProduct, string userId, string imagePath);

        void DeleteProduct(int id);

        void EditProduct(AddEditProductViewModel editProduct, int id, string userId);

        GetByIdAndLikeViewModel GetById(int id, string userId);

        IEnumerable<AllProductViewModel> GetAllProducts();

        GetByIdAndLikeViewModel Like(int id, string currentUserId);

        IEnumerable<MyProductsViewModel> MyProducts(string id);

        IEnumerable<MyProductsViewModel> Favorites(string id);


        IEnumerable<IndexRandomViewModel> RandomProducts(int count);

    }
}
