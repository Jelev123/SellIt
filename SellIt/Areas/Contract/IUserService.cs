namespace SellIt.Areas.Service
{
    using SellIt.Areas.ViewModel;
    using SellIt.Core.ViewModels.Product;

    public interface IUserService
    {
        Task CreateRoleAsync(RoleViewModel role);

        Task<IEnumerable<AllUsersViewModel>> AllUsersAsync();

        Task SetRoleAsync(string userId, AllUsersViewModel all);

        Task DeleteUserAsync(string userId);

        Task<UserByIdViewModel> UserByIdAsync(string userId);

        Task<IEnumerable<UserProductsViewModel>> UserProductsAsync(string userId);

        Task<IEnumerable<MyProductsViewModel>> MyProductsAsync(string id);

    }
}
