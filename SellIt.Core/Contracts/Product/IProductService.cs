namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        Task AddProduct(ProductViewModel addProduct, string userId, string imagePath);

        ProductViewModel GetById(int id, string userId);

        IEnumerable<ProductViewModel> GetAllProducts();

        ProductViewModel Like(int id, string currentUserId);

        IEnumerable<ProductViewModel> MyProducts(string id);
    }
}
