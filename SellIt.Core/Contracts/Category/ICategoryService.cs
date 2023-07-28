namespace SellIt.Core.Contracts.Category
{
    using SellIt.Core.ViewModels.Category;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryViewModel createCategory);
        Task<IEnumerable<AllCategoriesViewModel>> GetAllCategoriesAsync<T>();
    }
}
