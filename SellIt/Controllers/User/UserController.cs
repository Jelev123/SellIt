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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            await userService.CreateRoleAsync(role);

            return Redirect("/");
        }

        public async Task<IActionResult> AllUsers()
        {
            return View(await userService.AllUsersAsync());
        }


        public async Task<IActionResult> SetRole()
        {
            var roles = await userService.GetAllRolesAsync();
            ViewData["roles"] = roles.Select(s => new RoleViewModel
            {
                Name = s.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(string userId, AllUsersViewModel all)
        {
            return await userService.SetRoleAsync(userId, all)
                             .ContinueWith(_ => RedirectToAction("UserById", new { userId = all.UserId }));
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            return await userService.DeleteUserAsync(userId)
                             .ContinueWith(_ => RedirectToAction("AllUsers"));
        }


        public async Task<IActionResult> UserById(string userId)
        {
            return View(await userService.UserByIdAsync(userId));
        }


        public async Task<IActionResult> UserProducts(string userId)
        {
            return View(await userService.UserProductsAsync(userId));
        }


        public async Task<IActionResult> MyProducts()
        {
            return View(await userService.MyProductsAsync(userManager.GetUserId(User)));
        }
    }
}
