namespace SellIt.Core.Contracts.Count
{
    using SellIt.Core.ViewModels.Count;

    public interface ICountService
    {
        CountViewModel GetCount(int productId);
    }
}
