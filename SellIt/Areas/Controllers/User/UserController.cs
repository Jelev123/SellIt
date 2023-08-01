namespace SellIt.Areas.Controllers.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Areas.Service;
    using SellIt.Areas.ViewModel;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;


    public class UserController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UserController(ApplicationDbContext data, IUserService userService, UserManager<User> userManager)
        {
            this.data = data;
            this.userService = userService;
            this.userManager = userManager;
        }


        public async Task<IActionResult> CreateRole()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            await userService.CreateRoleAsync(role);
            return this.Redirect("/");
        }

        public async Task<IActionResult> AllUsers()
        {
            return this.View(await this.userService.AllUsersAsync());
        }

        public IActionResult SetRole()
        {
            var roles = this.data.Roles.ToList();

            this.ViewData["roles"] = roles.Select(s => new RoleViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(string userId, AllUsersViewModel all)
        {
            await this.userService.SetRoleAsync(userId, all);
            return this.RedirectToAction("UserById", new { userId = all.UserId });
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            await this.userService.DeleteUserAsync(userId);
            return this.RedirectToAction("AllUsers");
        }

        public async Task<IActionResult> UserById(string userId)
        {
            var user = await this.userService.UserByIdAsync(userId);
            return this.View(user);
        }

        public async Task<IActionResult> UserProducts(string userId)
        {
            var userProducts = await this.userService.UserProductsAsync(userId);
            return this.View(userProducts);
        }

        public async Task<IActionResult> MyProducts() => View(await this.userService.MyProductsAsync(userManager.GetUserId(User)));
    }
}
