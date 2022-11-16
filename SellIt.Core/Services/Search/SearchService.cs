namespace SellIt.Core.Services.Search
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

        public IEnumerable<SearchViewModel> SearchProduct(string searchName)
        {
            var searchedProducts = this.data.Products
                 .Select(s => new SearchViewModel
                 {
                     Name = s.Name,
                     CategoryName = s.Category.Name,
                     Description = s.Description,
                     UserId = s.UserId,
                     Id = s.Id,
                     Price = s.Price,
                     CoverPhoto =  s.Images.FirstOrDefault().URL
                 })
                 .Where(s => s.Name.Contains(searchName));

            return searchedProducts;
        }
    }
}
