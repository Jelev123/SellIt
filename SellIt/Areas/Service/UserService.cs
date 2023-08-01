﻿namespace SellIt.Areas.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SellIt.Areas.ViewModel;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICountService countService;


        public UserService(ApplicationDbContext data, RoleManager<IdentityRole> roleManager, ICountService countService)
        {
            this.data = data;
            this.roleManager = roleManager;
            this.countService = countService;
        }

        public async Task<IEnumerable<AllUsersViewModel>> AllUsersAsync()
             => await data.Users
                .Select(s => new AllUsersViewModel
                {
                    UserId = s.Id,
                    UserName = s.UserName,
                    DateCreated = s.DateCreated,
                    Email = s.Email,
                }).OrderBy(s => s.DateCreated)
            .ToListAsync();

        public async Task CreateRoleAsync(RoleViewModel role)
        {
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = role.Name,

            });
            await data.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            data.Remove(user);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyProductsViewModel>> MyProductsAsync(string userId)
           => await this.data.Products
                .Where(s => s.CreatedUserId == userId)
                .Select(x => new MyProductsViewModel
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    MessagesCount = x.Messages.Count,
                    Description = x.Description,
                    UserId = userId,
                    IsAprooved = x.IsAproved,
                    Price = x.Price,
                    CoverPhoto = x.Images.FirstOrDefault().URL
                }).ToListAsync();




        public async Task SetRoleAsync(string userId, AllUsersViewModel all)
        {
            var user = this.data.Users.FirstOrDefault(s => s.Id == userId);
            var role = this.data.Roles.FirstOrDefault(s => s.Name == all.RoleName);
            var userRoles = this.data.UserRoles.FirstOrDefault(s => s.UserId == user.Id);

            if (userRoles != null && userRoles.UserId.Contains(userId))
            {
                data.UserRoles.Remove(userRoles);
                await data.SaveChangesAsync();
                await data.UserRoles.AddAsync(new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                });
            }
            else
            {
                data.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                });
            }
            await data.SaveChangesAsync();
        }

        public async Task<UserByIdViewModel> UserByIdAsync(string userId)
        {
            var count = this.countService.GetUserProductsCount(userId);

            return await (from users in data.Users
                          from userRoles in data.UserRoles.Where(co => co.UserId == users.Id).DefaultIfEmpty()
                          from roles in data.Roles.Where(prod => prod.Id == userRoles.RoleId).DefaultIfEmpty()
                          from products in data.Products.Where(s => s.CreatedUserId == users.Id).DefaultIfEmpty()
                          select new UserByIdViewModel
                          {
                              UserId = users.Id,
                              UserName = users.UserName,
                              RoleName = roles.Name,
                              DateCreated = users.DateCreated,
                              Email = users.Email,
                              ProductName = products.Name,
                              ProductsCount = count.Result,

                          })
                       .Where(s => s.UserId == userId)
                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserProductsViewModel>> UserProductsAsync(string userId)
        => await data.Products
                .Where(s => s.CreatedUserId == userId)
                .Select(s => new UserProductsViewModel
                {
                    Id = s.ProductId,
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    Viewed = s.Viewed,
                    Price = s.Price,
                    CoverPhoto = s.Images.FirstOrDefault().URL,
                    IsAprooved = s.IsAproved,
                }).ToListAsync();
    }
}
