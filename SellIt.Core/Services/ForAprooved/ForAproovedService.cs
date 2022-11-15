namespace SellIt.Core.Services.ForAprooved
{
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ForAproovedService : IForAproovedService
    {
        private readonly ApplicationDbContext data;
        private readonly ICountService countService;
        public ForAproovedService(ApplicationDbContext data, ICountService countService)
        {
            this.data = data;
            this.countService = countService;
        }

        public IEnumerable<AllProductsViewModel> GetAllProductsForAproove()
        {
            var allProducts = this.data.Products
                .Where(s => s.IsAproved == false)
                .Select(s => new AllProductsViewModel
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    Id = s.Id,
                    IsAprooved = s.IsAproved,
                    Count = countService.GetCount().ProductsToAprooveCount,
                    CoverPhoto =  s.Images.FirstOrDefault().URL

                });
            return allProducts;
        }

        public async Task SetAproove(int id)
        {       
            var product = this.data.Products.FirstOrDefault(s => s.Id == id);
            product.IsAproved = true;
            data.SaveChangesAsync();
        }
    }
}
