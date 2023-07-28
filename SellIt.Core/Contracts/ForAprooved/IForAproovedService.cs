namespace SellIt.Core.Contracts.ForAprooved
{
    using SellIt.Core.ViewModels.Product;

    public interface IForAproovedService
    {
        Task<IEnumerable<AllProductsForAprooved>> GetAllProductsForAprooveAsync();

        Task SetAprooveAsync(int id);
    }
}
