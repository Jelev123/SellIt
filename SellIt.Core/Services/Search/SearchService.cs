namespace SellIt.Core.Services.Search
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public class SearchService : ISearchService
    {
        private readonly IRepository<Product> productRepository;

        public SearchService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<SearchViewModel>> SearchProductAsync(string searchName)       
            => await this.productRepository.AllAsNoTracking()
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
