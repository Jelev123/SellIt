namespace SellIt.Controllers.Admin
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Admin;
    using SellIt.Core.ViewModels.Admin;

    [Authorize(Roles = UserConstants.Role.Administrator)]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
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
            await adminService.CreateRoleAsync(role);

            return Redirect("/");
        }

        public async Task<IActionResult> AllUsers()
        {
            return View(await adminService.AllUsersAsync());
        }

        public async Task<IActionResult> SetRole()
        {
            var roles = await adminService.GetAllRolesAsync();
            ViewData["roles"] = roles.Select(s => new RoleViewModel
            {
                Name = s.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(string userId, AllUsersViewModel all)
        {
            return await adminService.SetRoleAsync(userId, all)
                             .ContinueWith(_ => RedirectToAction("UserById", new { userId = all.UserId }));
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            return await adminService.DeleteUserAsync(userId)
                             .ContinueWith(_ => RedirectToAction("AllUsers"));
        }

        public async Task<IActionResult> UserById(string userId)
        {
            return View(await adminService.UserByIdAsync(userId));
        }

        public async Task<IActionResult> UserProducts(string userId)
        {
            return View(await adminService.UserProductsAsync(userId));
        }
    }
}
