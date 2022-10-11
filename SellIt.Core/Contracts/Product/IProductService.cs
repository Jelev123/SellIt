namespace SellIt.Core.Contracts.Product
{
    using SellIt.Core.ViewModels.Product;

    public interface IProductService
    {
        Task AddProduct(AddProductViewModel addProduct, string userId, string imagePath);

    }
}
