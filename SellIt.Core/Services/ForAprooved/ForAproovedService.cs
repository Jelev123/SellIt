namespace SellIt.Core.Services.ForAprooved
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Constants.Error;
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ForAproovedService : IForAproovedService
    {
        private readonly IRepository<Product> productRepository;
        public ForAproovedService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<AllProductsForAprooved>> GetAllProductsForAprooveAsync()
        {
            return await productRepository.AllAsNoTracking()
                 .Where(s => s.IsAproved == false)
                 .Select(s => new AllProductsForAprooved
                 {
                     Name = s.Name,
                     CategoryName = s.Category.Name,
                     Id = s.ProductId,
                     CoverPhoto = s.Images.FirstOrDefault().URL
                 }).ToListAsync();
        }
        public async Task SetAprooveAsync(int id)
        {
            var product = await productRepository
                .All()
                .FirstOrDefaultAsync(s => s.ProductId == id)
                ?? throw new NullReferenceException(string.Format(
                             ErrorMessages.DataDoesNotExist,
                             typeof(Product).Name, "id", id));

            product.IsAproved = true;
            await productRepository.SaveChangesAsync();
        }
    }
}
