namespace SellIt.Core.Services.ForAprooved
{
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ForAproovedService : IForAproovedService
    {
        private readonly ApplicationDbContext data;

        public ForAproovedService(ApplicationDbContext data)
        {
            this.data = data;
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
                    Image = "/images/products/" + s.Images.FirstOrDefault().Id + s.Images.FirstOrDefault().Extension

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
