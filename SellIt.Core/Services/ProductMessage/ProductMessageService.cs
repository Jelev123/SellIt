namespace SellIt.Core.Services.ProductMessage
{
    using SellIt.Core.Contracts.ProductMessage;
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductMessageService : IProductMessageService
    {

        private readonly ApplicationDbContext data;

        public ProductMessageService(ApplicationDbContext data)
        {
            this.data = data;
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

        public IEnumerable<SendMessageViewModel> AllMessages(int id)
        {
            var product = this.data.Products.Where(s => s.Id == id).FirstOrDefault();

            var allMessages = this.data.ProductMessages
                .Where(s => s.ProductId == product.Id)
                .Select(s => new SendMessageViewModel
                {
                    Id = s.Id,
                    Text = s.Text,
                    ProductId = product.Id,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                });


            return allMessages;
        }
    }
}
