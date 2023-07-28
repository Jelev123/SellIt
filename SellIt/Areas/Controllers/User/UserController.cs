﻿namespace SellIt.Areas.Controllers.User
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
            await userService.CreateRole(role);
            return this.Redirect("/");
        }

        public IActionResult AllUsers()
        {
            return this.View(this.userService.AllUsers());
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
        public IActionResult SetRole(string userId, AllUsersViewModel all)
        {
            this.userService.SetRole(userId, all);
            return this.RedirectToAction("UserById", new  { userId = all.UserId});
        }

        public IActionResult DeleteUser(string userId)
        {   
            this.userService.DeleteUser(userId);
            return this.RedirectToAction("AllUsers");
        }

        public IActionResult UserById(string userId)
        {
           var user =  this.userService.UserByIdAsync(userId);
            return this.View(user);
        }

        public IActionResult UserProducts(string userId)
        {
            var userProducts = this.userService.UserProducts(userId);
            return this.View(userProducts);
        }

        public IActionResult MyProducts() => View(this.userService.MyProducts(userManager.GetUserId(User)));
    }
}
