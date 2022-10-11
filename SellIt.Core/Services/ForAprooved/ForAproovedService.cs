namespace SellIt.Core.Services.ForAprooved
{
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;

    public class ForAproovedService : IForAproovedService
    {
        private readonly ApplicationDbContext data;

        public ForAproovedService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<AddProductViewModel> GetAllProducts()
        {
            var allProducts = this.data.Products
                .Where(s => s.IsAproved == false)
                .Select(s => new AddProductViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                });
            return allProducts;
        }
    }
}
