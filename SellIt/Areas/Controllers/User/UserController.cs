namespace SellIt.Areas.Controllers.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Areas.Contract;
    using SellIt.Infrastructure.Data.Models;


    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;

        public UserController(RoleManager<IdentityRole> roleManager, IUserService userService, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> CreateRole()
        {

            await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Administrator"
            });

            return Ok();
        }

        public IActionResult AllUserMessages()
        {
            var userId = this.userManager.GetUserId(User);
            var all = this.userService.AllProductMessages(userId);

            return this.View(all);
        }
    }
}
