namespace SellIt.Core.Services.ForAprooved
{
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class ForAproovedService : IForAproovedService
    {
        private readonly ApplicationDbContext data;
        public ForAproovedService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<IEnumerable<AllProductsForAprooved>> GetAllProductsForAprooveAsync()
           => await this.data.Products
                .Where(s => s.IsAproved == false)
                .Select(s => new AllProductsForAprooved
                {
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    Id = s.ProductId,
                    CoverPhoto = s.Images.FirstOrDefault().URL
                }).ToListAsync();

        public async Task SetAprooveAsync(int id)
        {
            var product = await this.data.Products.FirstOrDefaultAsync(s => s.ProductId == id);
            product.IsAproved = true;
            await data.SaveChangesAsync();
        }
    }
}
