namespace SellIt.Core.Services.ProductMessage
{
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.ProductMessage;
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductMessageService : IProductMessageService
    {

        private readonly ApplicationDbContext data;
        private readonly ICountService countService;


        public ProductMessageService(ApplicationDbContext data, ICountService countService)
        {
            this.data = data;
            this.countService = countService;
        }

        public Task SendMessage(SendMessageViewModel sendMessage, string userId, int id)
        {        
            var product = this.data.Products.Where(s => s.Id == id).FirstOrDefault();
            var messages = new ProductMessages
            {
                UserId = userId,
                Text = sendMessage.Text,
                ProductId = product.Id,
                Product = product,
            };

          

            this.data.ProductMessages.Add(messages);
            this.data.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<SendMessageViewModel> AllMessages(int id, string userId)
        {
            var count = this.countService.GetCount();
            var allMessages = this.data.ProductMessages
                .Where(s => s.ProductId == id)
                .Select(s => new SendMessageViewModel
                {
                    Id = s.Id,
                    Text = s.Text,
                    ProductId = s.ProductId,
                    ProductName = s.Product.Name,
                    ProductImage = s.Product.Images.Select(s => s.URL).FirstOrDefault(),
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    ReplyedProductMessagesCount = count.ReplyedProductMessages,
                    ReplyProductMessages = s.ReplyProductMessages.Select(s => new ReplyProductMessagesViewModel
                    {
                        ProductMessageText = s.Text,
                        ReplyerUserName = s.User.UserName
                    }).ToList(),
                });
            return allMessages;
        }
    }
}
