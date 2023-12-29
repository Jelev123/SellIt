namespace SellIt.Controllers.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.ViewModels.User;
    using SellIt.Infrastructure.Data.Models;

    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult CreateRole()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return this.View(role);
            }
            await userService.CreateRoleAsync(role);

            return Redirect("/");
        }
            
        public async Task<IActionResult> AllUsers()
            => this.View(await this.userService.AllUsersAsync());
        

        public async Task<IActionResult> SetRole()
        {
            var roles = await this.userService.GetAllRolesAsync();
            this.ViewData["roles"] = roles.Select(s => new RoleViewModel
            {
                Name = s.Name,
            }).ToList();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(string userId, AllUsersViewModel all)
            => await userService.SetRoleAsync(userId, all)
            .ContinueWith(_ => RedirectToAction("UserById", new { userId = all.UserId }));
           

        public async Task<IActionResult> DeleteUser(string userId)
            => await this.userService.DeleteUserAsync(userId)
            .ContinueWith(_ => RedirectToAction("AllUsers"));
           

        public async Task<IActionResult> UserById(string userId)
            => this.View(await this.userService.UserByIdAsync(userId));
            

        public async Task<IActionResult> UserProducts(string userId)
            => this.View(await this.userService.UserProductsAsync(userId));


        public async Task<IActionResult> MyProducts()
            => this.View(await this.userService.MyProductsAsync(userManager.GetUserId(User)));
    }
}
