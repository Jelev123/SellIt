namespace SellIt.Core.Contracts.Admin
{
    using SellIt.Core.ViewModels.Admin;
    using SellIt.Infrastructure.Data.Models;

    public interface IAdminService
    {
        Task CreateRoleAsync(RoleViewModel role);

        Task<IEnumerable<AllUsersViewModel>> AllUsersAsync();

        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();

        Task SetRoleAsync(string userId, AllUsersViewModel all);

        Task DeleteUserAsync(string userId);

        Task<UserByIdViewModel> UserByIdAsync(string userId);

        Task<IEnumerable<UserProductsViewModel>> UserProductsAsync(string userId);

        string CurrentUserAccessor();

        string CurrentUserName();

        Task<User> GetCurrentUserAsync(string userId);
    }
}
