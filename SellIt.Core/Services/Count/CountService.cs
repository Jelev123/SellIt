namespace SellIt.Core.Services.Count
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Count;
    using SellIt.Infrastructure.Data.Models;

    public class CountService : ICountService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<ReplyMessage> replyMessagesRepository;

        public CountService(IRepository<Product> productRepository,
            IRepository<Message> messageRepository,
            IRepository<ReplyMessage> replyMessagesRepository)
        {
            this.productRepository = productRepository;
            this.messageRepository = messageRepository;
            this.replyMessagesRepository = replyMessagesRepository;
        }

        public async Task<CountViewModel> GetCountAsync(int productId)
        {
            return new CountViewModel
            {
                ProductsToAprooveCount = await productRepository.AllAsNoTracking().Where(s => s.IsAproved == false).CountAsync(),
                AllProducts = await productRepository.AllAsNoTracking().CountAsync(),
                ProductMessages = await messageRepository.AllAsNoTracking().Where(s => s.ProductId == productId).CountAsync()
                 + await replyMessagesRepository.AllAsNoTracking().Where(s => s.Message.ProductId == productId).CountAsync(),
            };
        }
        public async Task<int> GetUserProductsCountAsync(string userId)
        {
            return await productRepository
                .All()
                .Where(p => p.CreatedUserId == userId)
                .CountAsync();
        }
    }
}
