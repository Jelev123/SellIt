namespace SellIt.Core.Services.User
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Core.ViewModels.User;
    using SellIt.Infrastructure.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Product> productRepository;
        private readonly ICountService countService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserService(RoleManager<IdentityRole> roleManager, ICountService countService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IRepository<User> userRepository, IRepository<Product> productRepository, RoleManager<IdentityRole> roleManagerr)
        {
            this._roleManager = roleManager;
            this.countService = countService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            this.userRepository = userRepository;
            this.productRepository = productRepository;

        }

        public async Task<IEnumerable<AllUsersViewModel>> AllUsersAsync()
             => await userRepository.AllAsNoTracking()
                .Select(s => new AllUsersViewModel
                {
                    UserId = s.Id,
                    UserName = s.UserName,
                    DateCreated = s.DateCreated,
                    Email = s.Email,
                }).OrderBy(s => s.DateCreated)
            .ToListAsync();

        public async Task CreateRoleAsync(RoleViewModel role)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = role.Name,
            });
            await userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await this.GetCurrentUserAsync(userId);
            userRepository.Delete(user);
            await userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyProductsViewModel>> MyProductsAsync(string userId)
           => await this.productRepository.AllAsNoTracking()
                .Where(s => s.CreatedUserId == userId)
                .Select(x => new MyProductsViewModel
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    MessagesCount = x.Messages.Count,
                    Description = x.Description,
                    UserId = userId,
                    IsAprooved = x.IsAproved,
                    Price = x.Price,
                    CoverPhoto = x.Images.FirstOrDefault().URL
                }).ToListAsync();




        public async Task SetRoleAsync(string userId, AllUsersViewModel all)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(all.RoleName);

            if (user != null && role != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles != null && userRoles.Any())
                {
                    foreach (var userRole in userRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole);
                    }
                }

                await _userManager.AddToRoleAsync(user, role.Name);
                await userRepository.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }

        public async Task<UserByIdViewModel> UserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = string.Join(", ", roles);

            var productsCount = await countService.GetUserProductsCountAsync(userId);

            return new UserByIdViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                RoleName = roleName,
                DateCreated = user.DateCreated,
                Email = user.Email,
                ProductName = user.Products.FirstOrDefault()?.Name,
                ProductsCount = productsCount
            };
        }

        public async Task<IEnumerable<UserProductsViewModel>> UserProductsAsync(string userId)
        => await productRepository.AllAsNoTracking()
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
                }).ToListAsync();

        public string CurrentUserAccessor()
            => _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

        public string CurrentUserName()
           => _userManager.GetUserName(_httpContextAccessor.HttpContext.User);

        public async Task<User> GetCurrentUserAsync(string userId)
        => await this.userRepository.AllAsNoTracking().FirstOrDefaultAsync(s => s.Id == userId);

        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        => await _roleManager.Roles
        .Select(r => new RoleViewModel
        {
            Name = r.Name
        })
        .ToListAsync();


    }
}
