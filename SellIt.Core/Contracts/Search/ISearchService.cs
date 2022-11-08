namespace SellIt.Core.Contracts.Search
{
    using SellIt.Core.ViewModels.Product;

    public interface ISearchService
    {
        IEnumerable<AllProductsViewModel> SearchProduct(string name);

    }
}
