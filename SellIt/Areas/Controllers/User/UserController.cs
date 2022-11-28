namespace SellIt.Areas.Controllers.User
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
 
    using SellIt.Infrastructure.Data.Models;


    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
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
    }
}
