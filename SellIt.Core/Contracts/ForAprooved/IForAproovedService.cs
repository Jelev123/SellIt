namespace SellIt.Core.Contracts.ForAprooved
{
    using SellIt.Core.ViewModels.Product;

    public interface IForAproovedService
    {
        IEnumerable<AddProductViewModel> GetAllProducts();
    }
}
