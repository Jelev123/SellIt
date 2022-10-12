namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        Task AddProduct(AddProductViewModel addProduct, string userId, string imagePath);

        AllProductsViewModel GetById(int id, string userId);

        IEnumerable<AllProductsViewModel> MyProducts(string id);

    }
}
