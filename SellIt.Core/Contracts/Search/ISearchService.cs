namespace SellIt.Core.Contracts.Search
{
    using SellIt.Core.ViewModels.Product;

    public interface ISearchService
    {
        IEnumerable<SearchViewModel> SearchProduct(string searchName);

    }
}
