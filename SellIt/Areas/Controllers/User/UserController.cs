namespace SellIt.Areas.Controllers.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Areas.User.Contracts;
    using SellIt.Areas.User.ViewModels;
    using SellIt.Infrastructure.Data.Models;


    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUserService userService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> CreateRole()
        {

            await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Administrator"
            });

            return Ok();
        }

        public IActionResult SendMessage()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel send)
        {
            var userId = this.userManager.GetUserId(User);
            var userName = this.userManager.GetUserName(User);

            var sendMessage = this.userService.SendMessage(send, userId,userName);
            return this.Redirect("/");
        }
        public IActionResult GetAllProductMessages(int id)
        {
            var allMessages = this.userService.GetAllProductMessages(id);
            return this.View(allMessages);
        }
    }
}
