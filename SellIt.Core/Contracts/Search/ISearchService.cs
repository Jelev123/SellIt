namespace SellIt.Core.Contracts.Search
{
    using SellIt.Core.ViewModels.Product;

    public interface ISearchService
    {
        Task<IEnumerable<SearchViewModel>> SearchProductAsync(string searchName);

    }
}
