﻿namespace SellIt.Core.Services.Search
{
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext data;

        public SearchService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<AllProductsViewModel> SearchProduct(string searchName)
        {
            var searchedProducts = this.data.Products
                 .Select(s => new AllProductsViewModel
                 {
                     Name = s.Name,
                     CategoryName = s.Category.Name,
                     Description = s.Description,
                     IsAprooved = s.IsAproved,
                     LikedCount = s.LikedCount,
                     Viewed = s.Viewed,
                     UserId = s.UserId,
                     Id = s.Id,
                     Image = "/images/products/" + s.Images.FirstOrDefault().Id + "." + s.Images.FirstOrDefault().Extension,
                 })
                 .Where(s => s.Name.Contains(searchName));

            return searchedProducts;
        }
    }
}
