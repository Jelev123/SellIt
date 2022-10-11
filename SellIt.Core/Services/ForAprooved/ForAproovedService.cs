namespace SellIt.Core.Services.ForAprooved
{
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public class ForAproovedService : IForAproovedService
    {
        private readonly ApplicationDbContext data;

        public ForAproovedService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<AllProductsViewModel> GetAllProducts()
        {
            var allProducts = this.data.Products
                .Where(s => s.IsAproved == false)
                .Select(s => new AllProductsViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    Image = "/images/products/" + s.Images.FirstOrDefault().Id + "." + s.Images.FirstOrDefault().Extension,

                });
            return allProducts;
        }
    }
}
