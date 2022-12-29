namespace SellIt.Areas.Service
{
    using SellIt.Areas.ViewModel;

    public interface IUserService
    {
        Task CreateRole(RoleViewModel role);

        IEnumerable<AllUsersViewModel> AllUsers();

        Task SetRole(string userId, AllUsersViewModel all);

        Task DeleteUser(string userId);

        UserByIdViewModel UserById(string userId);

        IEnumerable<UserProductsViewModel> UserProducts(string userId);
    }
}
