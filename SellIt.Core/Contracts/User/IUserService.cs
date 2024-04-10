namespace SellIt.Core.Contracts.User
{
    using SellIt.Core.ViewModels.Product;

    public interface IUserService
    {
        Task<IEnumerable<MyProductsViewModel>> MyProductsAsync(string id);   
    }
}
