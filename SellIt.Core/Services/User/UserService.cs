namespace SellIt.Core.Services.User
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<Product> productRepository;

        public UserService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<MyProductsViewModel>> MyProductsAsync(string userId)
        {
            return await productRepository.AllAsNoTracking()
               .Where(s => s.CreatedUserId == userId)
               .Select(x => new MyProductsViewModel
               {
                   Id = x.ProductId,
                   Name = x.Name,
                   CategoryName = x.Category.Name,
                   MessagesCount = x.Messages.Count,
                   Description = x.Description,
                   UserId = userId,
                   IsAprooved = x.IsAproved,
                   Price = x.Price,
                   CoverPhoto = x.Images.FirstOrDefault().URL,
               }).ToListAsync();
        }
    }
}
