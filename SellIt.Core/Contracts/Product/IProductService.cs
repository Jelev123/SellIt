namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        void AddProduct(AddEditProductViewModel addProduct);

        void DeleteProduct(int id);

        void EditProduct(AddEditProductViewModel editProduct, int id);

        GetByIdAndLikeViewModel GetById(int id);

        IEnumerable<AllProductViewModel> GetAllProducts();

        IEnumerable<AllProductViewModel> GetAllProductsByCategoryId(int id);

        GetByIdAndLikeViewModel Like(int id);

        IEnumerable<MyProductsViewModel> Favorites();

        IEnumerable<IndexRandomViewModel> RandomProducts(int count);

    }
}
