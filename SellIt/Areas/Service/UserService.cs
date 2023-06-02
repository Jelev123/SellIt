namespace SellIt.Areas.Service
{
    using Microsoft.AspNetCore.Identity;
    using SellIt.Areas.ViewModel;
    using SellIt.Core.Contracts.Count;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICountService countService;


        public UserService(ApplicationDbContext data, RoleManager<IdentityRole> roleManager, ICountService countService)
        {
            this.data = data;
            this.roleManager = roleManager;
            this.countService = countService;
        }

        public IEnumerable<AllUsersViewModel> AllUsers()
        {
            var allUsers = data.Users
                .Select(s => new AllUsersViewModel
                {
                    UserId = s.Id,
                    UserName = s.UserName,
                    DateCreated = s.DateCreated,
                    Email = s.Email,
                }).OrderBy(s => s.DateCreated);
            return allUsers;
        }

        public async Task CreateRole(RoleViewModel role)
        {
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = role.Name,

            });
            data.SaveChanges();
        }

        public async Task DeleteUser(string userId)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            data.Remove(user);
            data.SaveChanges();
        }

        public async Task SetRole(string userId, AllUsersViewModel all)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            var role = this.data.Roles.FirstOrDefault(s => s.Name == all.RoleName);
            var userRoles = this.data.UserRoles.FirstOrDefault(s => s.UserId == user.Id);

            if (userRoles != null && userRoles.UserId.Contains(userId))
            {
                data.UserRoles.Remove(userRoles);
                data.SaveChanges();
                data.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                });
            }
            else
            {
                data.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                });
            }
            data.SaveChanges();
        }

        public UserByIdViewModel UserById(string userId)
        {
            var count = this.countService.GetUserProductsCount(userId);

            var user = (from users in data.Users
                        from userRoles in data.UserRoles.Where(co => co.UserId == users.Id).DefaultIfEmpty()
                        from roles in data.Roles.Where(prod => prod.Id == userRoles.RoleId).DefaultIfEmpty()
                        from products in data.Products.Where(s => s.CreatedUserId == users.Id).DefaultIfEmpty()
                        select new UserByIdViewModel
                        {
                            UserId = users.Id,
                            UserName = users.UserName,
                            RoleName = roles.Name,
                            DateCreated = users.DateCreated,
                            Email = users.Email,
                            ProductName = products.Name,
                            ProductsCount = count,
                            
                        })
                       .Where(s => s.UserId == userId)
                       .FirstOrDefault();
            return user;
        }

        public IEnumerable<UserProductsViewModel> UserProducts(string userId)
        {
            var userProducts = data.Products
                .Where(s => s.CreatedUserId == userId)
                .Select(s => new UserProductsViewModel
                {
                    Id = s.ProductId,
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    Viewed = s.Viewed,
                    Price = s.Price,
                    CoverPhoto = s.Images.FirstOrDefault().URL,
                    IsAprooved = s.IsAproved,
                });
            return userProducts;
        }
    }
}
