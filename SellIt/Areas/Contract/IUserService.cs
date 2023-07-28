namespace SellIt.Areas.Service
{
    using SellIt.Areas.ViewModel;
    using SellIt.Core.ViewModels.Product;

    public interface IUserService
    {
        Task CreateRole(RoleViewModel role);

        IEnumerable<AllUsersViewModel> AllUsers();

        Task SetRole(string userId, AllUsersViewModel all);

        Task DeleteUser(string userId);

        Task<UserByIdViewModel> UserByIdAsync(string userId);

        IEnumerable<UserProductsViewModel> UserProducts(string userId);

        IEnumerable<MyProductsViewModel> MyProducts(string id);

    }
}
