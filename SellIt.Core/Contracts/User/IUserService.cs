namespace SellIt.Core.Contracts.User
{
    using SellIt.Core.ViewModels.Product;
    using SellIt.Core.ViewModels.User;
    using SellIt.Infrastructure.Data.Models;

    public interface IUserService
    {
        Task CreateRoleAsync(RoleViewModel role);

        Task<IEnumerable<AllUsersViewModel>> AllUsersAsync();

        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();

        Task SetRoleAsync(string userId, AllUsersViewModel all);

        Task DeleteUserAsync(string userId);

        Task<UserByIdViewModel> UserByIdAsync(string userId);

        Task<IEnumerable<UserProductsViewModel>> UserProductsAsync(string userId);

        Task<IEnumerable<MyProductsViewModel>> MyProductsAsync(string id);

        string CurrentUserAccessor();

        string CurrentUserName();

        Task<User> GetCurrentUserAsync(string userId);
    }
}
