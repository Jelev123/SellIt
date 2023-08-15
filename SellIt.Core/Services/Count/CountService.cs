namespace SellIt.Core.Services.Count
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.ViewModels.Count;
    using SellIt.Infrastructure.Data;

    public class CountService : ICountService
    {
        private readonly ApplicationDbContext data;

        public CountService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<CountViewModel> GetCountAsync(int productId)
            => new CountViewModel
            {
                ProductsToAprooveCount = await this.data.Products.Where(s => s.IsAproved == false).CountAsync(),
                AllProducts = await this.data.Products.CountAsync(),
                ProductMessages = await this.data.Messages.Where(s => s.ProductId == productId).CountAsync()
                + await this.data.ReplyMessages.Where(s => s.Message.ProductId == productId).CountAsync(),
            };

        public async Task<int> GetUserProductsCountAsync(string userId)
            => await this.data.Products.Where(p => p.CreatedUserId == userId).CountAsync();
    }
}
