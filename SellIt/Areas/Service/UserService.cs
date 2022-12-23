namespace SellIt.Areas.Service
{
    using Microsoft.AspNetCore.Identity;
    using SellIt.Areas.ViewModel;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserService(ApplicationDbContext data, RoleManager<IdentityRole> roleManager)
        {
            this.data = data;
            this.roleManager = roleManager;
        }

        public IEnumerable<AllUsersViewModel> AllUsers()
        {
            var allUsers = data.Users
                .Select(s => new AllUsersViewModel
                {
                    UserId = s.Id,
                    UserName = s.UserName,
                });

            return allUsers;
        }

        public async Task CreateRole(RoleViewModel role)
        {
           await roleManager.CreateAsync(new IdentityRole
            {
                Name = role.Name
            });
            data.SaveChanges();
        }

        public async Task SetRole(string userId, AllUsersViewModel all)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            var role = this.data.Roles.FirstOrDefault(s => s.Name == all.RoleName);

            data.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id
            });
            data.SaveChanges();
        }
    }
}
