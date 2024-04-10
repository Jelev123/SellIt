namespace SellIt.Controllers.User
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.User;
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

        [Authorize]
        public async Task<IActionResult> MyProducts()
        {
            return View(await userService.MyProductsAsync(userManager.GetUserId(User)));
        }
    }
}
