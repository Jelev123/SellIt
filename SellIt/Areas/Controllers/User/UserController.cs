namespace SellIt.Areas.Controllers.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SellIt.Areas.Service;
    using SellIt.Areas.ViewModel;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;


    public class UserController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;



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
            await userService.CreateRole(role);
            return this.Redirect("/");
        }

        public IActionResult AllUsers(AllUsersViewModel all)
        {
            return this.View(this.userService.AllUsers());
        }

        public IActionResult SetRole(string userId)
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
        public IActionResult SetRole(string userId, AllUsersViewModel all)
        {
            this.userService.SetRole(userId, all);
            return this.Redirect("/");
        }

        public IActionResult DeleteUser(string userId)
        {
            
            this.userService.DeleteUser(userId);
            return this.RedirectToAction("AllUsers");
        }
    }
}
