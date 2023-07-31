namespace SellIt.Core.Contracts.Count
{
    using SellIt.Core.ViewModels.Count;

    public interface ICountService
    {
        Task<CountViewModel> GetCount(int productId);

        Task<int> GetUserProductsCount(string userId);
    }
}
