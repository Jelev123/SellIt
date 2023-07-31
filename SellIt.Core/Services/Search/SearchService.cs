namespace SellIt.Core.Services.Search
{
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext data;

        public SearchService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<IEnumerable<SearchViewModel>> SearchProductAsync(string searchName)       
            => await this.data.Products
                 .Select(s => new SearchViewModel
                 {
                     Name = s.Name,
                     CategoryName = s.Category.Name,
                     Description = s.Description,
                     UserId = s.CreatedUserId,
                     Id = s.ProductId,
                     Price = s.Price,
                     CoverPhoto =  s.Images.FirstOrDefault().URL
                 })
                 .Where(s => (s.Name.Contains(searchName)) || (s.CategoryName.Contains(searchName)))
            .ToListAsync();
    }
}
