namespace SellIt.Core.Contracts.Count
{
    using SellIt.Core.ViewModels.Count;

    public interface ICountService
    {
        Task<CountViewModel> GetCountAsync(int productId);

        Task<int> GetUserProductsCountAsync(string userId);
    }
}
